using System.Collections.Generic;
using System.Linq;
using ItemInventory;
using Items;
using Items.Avatar;
using UI.PlayerInventory;
using UnityEngine;
using WindowManagement;

namespace Character.Player.InventoryManagement
{
    public class PlayerInventory : MonoBehaviour, IInventoryProvider, IEquippedItems, IPlayerInventoryWindowDataSource,
        IPlayerInventoryMessageListener<PlayerChangeEquippedItemMessage>,
        IPlayerInventoryMessageListener<PlayerCurrencyUpdateRequestMessage>
    {
        [SerializeField] private ItemDatabase _itemDatabase;
        [SerializeField] private KeyCode _openInventoryKey;

        private readonly Inventory _inventory = new();

        private readonly Dictionary<ItemSlotType, int> _equippedItems = new()
        {
            { ItemSlotType.Hat, 0 },
            { ItemSlotType.HeadFace, 0 },
            { ItemSlotType.UpperBody, 0 },
            { ItemSlotType.Hands, 0 },
            { ItemSlotType.LowerBody, 0 },
            { ItemSlotType.Feet, 0 },
            { ItemSlotType.Holdable, 0 }
        };

        private void Start()
        {
            _inventory.OnItemAdded += OnItemAdded;
            _inventory.OnItemRemoved += OnItemRemoved;
            PlayerInventoryBroadcaster.Instance.Subscribe<PlayerChangeEquippedItemMessage>(this);
        }

        private void OnDestroy()
        {
            _inventory.OnItemAdded -= OnItemAdded;
            _inventory.OnItemRemoved -= OnItemRemoved;
            PlayerInventoryBroadcaster.Instance.Unsubscribe<PlayerChangeEquippedItemMessage>(this);
        }

        private void Update()
        {
            if (!Input.GetKeyDown(_openInventoryKey)) return;

            WindowManagementBroadcaster.Instance.Broadcast(
                new ShowWindowMessage(WindowType.PlayerInventory,
                    intent: new PlayerInventoryWindowIntent(this)));
        }

        private void OnItemAdded(InventoryEntry item)
        {
            if (NotifyIfCurrencyUpdate(item)) return;

            PlayerInventoryBroadcaster.Instance.Broadcast(new PlayerInventoryUpdateMessage());
        }

        private bool NotifyIfCurrencyUpdate(InventoryEntry item)
        {
            AItem foundItem = _itemDatabase.GetItem(item.Id);

            if (!(foundItem is CurrencyItem currency)) return false;

            PlayerInventoryBroadcaster.Instance.Broadcast(new PlayerCurrencyUpdateMessage(currency, item.Amount));
            return true;
        }

        private void OnItemRemoved(InventoryEntry item)
        {
            if (NotifyIfCurrencyUpdate(item)) return;

            foreach (KeyValuePair<ItemSlotType, int> slotItemPair in _equippedItems)
            {
                if (slotItemPair.Value == item.Id)
                {
                    PlayerInventoryBroadcaster.Instance.Broadcast(
                        new PlayerChangeEquippedItemMessage(slotItemPair.Key, 0));
                    break;
                }
            }

            PlayerInventoryBroadcaster.Instance.Broadcast(new PlayerInventoryUpdateMessage());
        }

        public IReadOnlyInventory GetInventory()
        {
            return _inventory;
        }

        public int GetEquippedItem(ItemSlotType slotType)
        {
            return _equippedItems.TryGetValue(slotType, out int itemId) ? itemId : 0;
        }

        public IEnumerable<int> GetAllEquippedItems()
        {
            return _equippedItems.Values;
        }

        public IEnumerable<InventoryListEntry> GetAllItems()
        {
            foreach (InventoryEntry entry in _inventory)
            {
                AItem item = GetItem(entry.Id);
                if (item is CurrencyItem || _equippedItems.ContainsValue(item.Id)) continue;
                yield return new InventoryListEntry { Item = item, Amount = entry.Amount };
            }
        }

        public IReadOnlyDictionary<ItemSlotType, AAvatarItem> GetEquippedItems()
        {
            return _equippedItems.ToDictionary(pair => pair.Key, pair => GetItem(pair.Value) as AAvatarItem);
        }

        public AItem GetItem(int id)
        {
            return _itemDatabase.GetItem(id);
        }

        public void OnMessageReceived(PlayerChangeEquippedItemMessage message)
        {
            var foundItem = _itemDatabase.GetItem(message.NewEquippedItemId) as AAvatarItem;

            if (foundItem != null && foundItem.SlotType != message.SlotToChange) return;

            _equippedItems[message.SlotToChange] = foundItem != null ? message.NewEquippedItemId : 0;

            PlayerInventoryBroadcaster.Instance.Broadcast(
                new PlayerEquippedItemUpdatedMessage(
                    message.SlotToChange,
                    foundItem != null && _inventory.HasItem(message.NewEquippedItemId) ? foundItem : null
                ));
        }

        public void OnMessageReceived(PlayerCurrencyUpdateRequestMessage message)
        {
            if (message.Currency == null)
            {
                CurrencyItem currencyItem = null;
                int currencyAmount = 0;
                foreach (InventoryEntry entry in _inventory)
                {
                    if (GetItem(entry.Id) is CurrencyItem item)
                    {
                        currencyItem = item;
                        currencyAmount = entry.Amount;
                        break;
                    }
                }

                PlayerInventoryBroadcaster.Instance.Broadcast(
                    new PlayerCurrencyUpdateMessage(currencyItem, currencyAmount));

                return;
            }

            PlayerInventoryBroadcaster.Instance.Broadcast(
                new PlayerCurrencyUpdateMessage(
                    message.Currency,
                    _inventory.GetItemAmount(message.Currency.Id)));
        }
    }
}