using System;
using System.Collections.Generic;
using Character.Player.InventoryManagement;
using Items;
using UnityEngine;
using UnityEngine.UI;
using WindowManagement;

namespace UI.Stores.ClothingShop
{
    public class ClothingShopWindowView : MonoBehaviour,
        IPlayerInventoryMessageListener<PlayerCurrencyUpdateMessage>
    {
        [SerializeField] private CellListView<StoreItemCell, ShopItemData> _purchaseItemsListView;
        [SerializeField] private CellListView<StoreItemCell, AItem> _playerItemsListView;
        [SerializeField] private Button _closeWindowButton;

        public event Action OnCloseWindowButtonClick;
        public event Action<ShopItemData> OnPurchaseItemCellSelect;
        public event Action<AItem> OnSellItemCellSelect;

        private void Awake()
        {
            _closeWindowButton.onClick.AddListener(OnCloseWindowButtonClicked);

            _purchaseItemsListView.OnSelectCell += OnPurchasingItemCellSelect;
            _playerItemsListView.OnSelectCell += OnSellingItemCellSelect;

            PlayerInventoryBroadcaster.Instance.Subscribe(this);
        }

        private void OnDestroy()
        {
            PlayerInventoryBroadcaster.Instance.Unsubscribe(this);
        }

        private void OnPurchasingItemCellSelect(ShopItemData shopItemData)
        {
            OnPurchaseItemCellSelect?.Invoke(shopItemData);
        }

        private void OnSellingItemCellSelect(AItem item)
        {
            OnSellItemCellSelect?.Invoke(item);
        }

        private void OnCloseWindowButtonClicked()
        {
            OnCloseWindowButtonClick?.Invoke();
            WindowManagementBroadcaster.Instance.Broadcast(new CloseWindowMessage());
        }

        public void Setup(IEnumerable<ShopItemData> itemsOnShop, IEnumerable<AItem> playerItems)
        {
            _purchaseItemsListView.PopulateList(itemsOnShop, SetupPurchaseItemListingCell);
            _playerItemsListView.PopulateList(playerItems, SetupPlayerItemListingCell);
        }

        private void SetupPlayerItemListingCell(StoreItemCell cell, AItem item, Action<StoreItemCell> onSelectCallback)
        {
            cell.Setup(item, item.Price, true, onSelectCallback);
        }

        private void SetupPurchaseItemListingCell(StoreItemCell cell, ShopItemData data,
            Action<StoreItemCell> onSelectCallback)
        {
            cell.Setup(data.Item, data.ShopPrice, data.CanBuy, onSelectCallback);
        }

        public void OnMessageReceived(PlayerCurrencyUpdateMessage message)
        {
            foreach (KeyValuePair<StoreItemCell, ShopItemData> cellPair in _purchaseItemsListView.CellDataMap)
            {
                if (cellPair.Value.ShopPrice.Type.Id != message.Currency.Id) continue;

                cellPair.Key.SetCellState(message.CurrentAmount >= cellPair.Value.ShopPrice.Value);
            }
        }
    }
}