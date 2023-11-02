using System.Collections.Generic;
using ItemInventory;
using Items;
using Items.Avatar;
using UnityEngine;

namespace Character.Player.InventoryManagement
{
    public class PlayerInventory : MonoBehaviour, IInventoryProvider,
        IPlayerInventoryMessageListener<PlayerChangeEquippedItem>
    {
        [SerializeField] private ItemCollection _itemDatabase;

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
            PlayerInventoryBroadcaster.Instance.Subscribe<PlayerChangeEquippedItem>(this);
        }

        private void OnDestroy()
        {
            _inventory.OnItemAdded -= OnItemAdded;
            _inventory.OnItemRemoved -= OnItemRemoved;
            PlayerInventoryBroadcaster.Instance.Unsubscribe<PlayerChangeEquippedItem>(this);
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
                    PlayerInventoryBroadcaster.Instance.Broadcast(new PlayerChangeEquippedItem(slotItemPair.Key, 0));
                    break;
                }
            }

            PlayerInventoryBroadcaster.Instance.Broadcast(new PlayerInventoryUpdateMessage());
        }

        public IReadOnlyInventory GetInventory()
        {
            return _inventory;
        }

        public void OnMessageReceived(PlayerChangeEquippedItem message)
        {
            var foundItem = _itemDatabase.GetItem(message.NewEquippedItemId) as AAvatarItem;

            if (foundItem != null && foundItem.SlotType != message.SlotToChange) return;

            _equippedItems[message.SlotToChange] = foundItem != null ? message.NewEquippedItemId : 0;

            PlayerInventoryBroadcaster.Instance.Broadcast(
                new PlayerEquippedItemUpdated(
                    message.SlotToChange,
                    foundItem != null && _inventory.HasItem(message.NewEquippedItemId) ? foundItem : null
                ));
        }
    }
}