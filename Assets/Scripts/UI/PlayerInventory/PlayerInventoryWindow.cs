using System;
using Character.Player.InventoryManagement;
using Items.Avatar;
using UnityEngine;
using WindowManagement;

namespace UI.PlayerInventory
{
    public class PlayerInventoryWindow : AWindowController,
        IPlayerInventoryMessageListener<PlayerInventoryUpdateMessage>,
        IPlayerInventoryMessageListener<PlayerChangeEquippedItemMessage>
    {
        [SerializeField] private PlayerInventoryWindowView _windowView;

        private IPlayerInventoryWindowDataSource _dataSource;

        private void Awake()
        {
            _windowView.OnUnEquipItemSlotCellSelected += OnUnEquipItemSlotCellSelected;
            _windowView.OnEquipItemCellSelect += OnEquipItemCellSelect;

            PlayerInventoryBroadcaster.Instance.Subscribe<PlayerInventoryUpdateMessage>(this);
            PlayerInventoryBroadcaster.Instance.Subscribe<PlayerChangeEquippedItemMessage>(this);
        }

        private void OnDestroy()
        {
            PlayerInventoryBroadcaster.Instance.Unsubscribe<PlayerInventoryUpdateMessage>(this);
            PlayerInventoryBroadcaster.Instance.Unsubscribe<PlayerChangeEquippedItemMessage>(this);
        }

        private void OnUnEquipItemSlotCellSelected(ItemSlotType slotType)
        {
            PlayerInventoryBroadcaster.Instance.Broadcast(new PlayerChangeEquippedItemMessage(slotType, 0));
        }

        private void OnEquipItemCellSelect(InventoryListEntry entry)
        {
            if (!(entry.Item is AAvatarItem avatarItem)) return;

            PlayerInventoryBroadcaster.Instance.Broadcast(
                new PlayerChangeEquippedItemMessage(avatarItem.SlotType, entry.Item.Id));
        }

        protected internal override void Show(IWindowIntent intent = null)
        {
            if (intent == null) throw new ArgumentNullException();
            if (!(intent is PlayerInventoryWindowIntent windowIntent)) throw new InvalidCastException();

            _dataSource = windowIntent.DataSource;

            SetupView();
        }

        private void SetupView()
        {
            _windowView.Setup(_dataSource.GetAllItems(), _dataSource.GetEquippedItems());
        }

        protected internal override void OnCloseWindow()
        {
        }

        public void OnMessageReceived(PlayerInventoryUpdateMessage message)
        {
            // Not ideal, but it'll do for now
            SetupView();
        }

        public void OnMessageReceived(PlayerChangeEquippedItemMessage message)
        {
            // Again, not ideal ...
            SetupView();
        }
    }
}