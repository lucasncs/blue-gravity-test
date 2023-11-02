using System;
using System.Collections.Generic;
using Character.Player.InventoryManagement;
using Character.Player.PurchaseManagement;
using Items;
using Money;
using UnityEngine;
using WindowManagement;

namespace UI.Stores.ClothingShop
{
    public class ClothingShopWindow : AWindowController,
        IPlayerInventoryMessageListener<PlayerInventoryUpdateMessage>
    {
        [SerializeField] private ClothingShopWindowView _windowView;

        private IClothingShopWindowDataSource _dataSource;

        private void Awake()
        {
            _windowView.OnCloseWindowButtonClick += OnCloseWindowButtonClicked;
            _windowView.OnPurchaseItemCellSelect += OnBuyItemCellSelected;
            _windowView.OnSellItemCellSelect += OnSellItemCellSelected;

            PlayerInventoryBroadcaster.Instance.Subscribe(this);
        }

        private void OnDestroy()
        {
            PlayerInventoryBroadcaster.Instance.Unsubscribe(this);
        }

        private void OnCloseWindowButtonClicked()
        {
        }

        private void OnBuyItemCellSelected(ShopItemData shopItemData)
        {
            PurchaseBroadcaster.Instance.Broadcast(
                new PurchaseItemMessage(
                    shopItemData.Item,
                    shopItemData.ShopPrice,
                    _dataSource.GetInventory()));
        }

        private void OnSellItemCellSelected(AItem item)
        {
            PurchaseBroadcaster.Instance.Broadcast(
                new SellItemMessage(
                    item,
                    _dataSource.GetInventory()));
        }

        protected internal override void Show(IWindowIntent intent = null)
        {
            if (intent == null) throw new ArgumentNullException();
            if (!(intent is ClothingShopWindowIntent windowIntent)) throw new InvalidCastException();

            _dataSource = windowIntent.DataSource;

            SetupView();
        }

        private void SetupView()
        {
            var shopItemsDataList = new List<ShopItemData>(_dataSource.GetAllItemsOnShop().Length);
            foreach (AItem item in _dataSource.GetAllItemsOnShop())
            {
                if (!item.Price.IsValid()) continue;

                int currentMoney = _dataSource.GetInventory().GetItemAmount(item.Price.Type.Id);
                Price itemPurchasePrice = _dataSource.CalculateSellingPrice(item.Price);
                shopItemsDataList.Add(new ShopItemData
                {
                    Item = item,
                    ShopPrice = itemPurchasePrice,
                    CanBuy = currentMoney >= itemPurchasePrice.Value
                });
            }

            _windowView.Setup(shopItemsDataList, _dataSource.GetAllItems());
        }

        protected internal override void OnCloseWindow()
        {
        }

        public void OnMessageReceived(PlayerInventoryUpdateMessage message)
        {
            // Not ideal, but it'll do for now
            SetupView();
        }
    }
}