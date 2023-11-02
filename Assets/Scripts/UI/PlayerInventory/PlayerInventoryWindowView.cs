using System;
using System.Collections.Generic;
using Character.Player.InventoryManagement;
using Items.Avatar;
using UnityEngine;
using UnityEngine.UI;
using WindowManagement;

namespace UI.PlayerInventory
{
    public class PlayerInventoryWindowView : MonoBehaviour,
        IPlayerInventoryMessageListener<PlayerEquippedItemUpdatedMessage>
    {
        [SerializeField] private EquippedItemSlotsView _equippedItemSlots;
        [SerializeField] private CellListView<InventoryItemCell, InventoryListEntry> _itemListing;
        [SerializeField] private Button _closeWindowButton;

        public event Action<ItemSlotType> OnUnEquipItemSlotCellSelected;
        public event Action<InventoryListEntry> OnEquipItemCellSelect;
        public event Action OnCloseWindowButtonClick;

        private void Awake()
        {
            _closeWindowButton.onClick.AddListener(OnCloseWindowButtonClicked);

            PlayerInventoryBroadcaster.Instance.Subscribe(this);

            _equippedItemSlots.OnItemSlotCellSelect += OnItemSlotCellSelected;
            _itemListing.OnSelectCell += OnListItemCellSelect;
        }

        private void OnItemSlotCellSelected(ItemSlotType slotType)
        {
            OnUnEquipItemSlotCellSelected?.Invoke(slotType);
        }

        private void OnListItemCellSelect(InventoryListEntry entry)
        {
            OnEquipItemCellSelect?.Invoke(entry);
        }

        private void OnDestroy()
        {
            PlayerInventoryBroadcaster.Instance.Unsubscribe(this);
        }

        public void Setup(IEnumerable<InventoryListEntry> items,
            IReadOnlyDictionary<ItemSlotType, AAvatarItem> equippedItems)
        {
            _equippedItemSlots.Setup(equippedItems);
            _itemListing.PopulateList(items, SetupItemListingCell);
        }

        private void SetupItemListingCell(InventoryItemCell cell, InventoryListEntry entry,
            Action<InventoryItemCell> callback)
        {
            cell.Setup(entry.Item, entry.Amount, callback);
        }

        private void OnCloseWindowButtonClicked()
        {
            OnCloseWindowButtonClick?.Invoke();
            WindowManagementBroadcaster.Instance.Broadcast(new CloseWindowMessage());
        }

        public void OnMessageReceived(PlayerEquippedItemUpdatedMessage message)
        {
            _equippedItemSlots.ShowItem(message.NewEquippedItem, message.SlotType);
        }
    }
}