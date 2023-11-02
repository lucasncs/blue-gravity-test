using System;
using System.Collections.Generic;
using Character.Player.PurchaseManagement;
using Items;
using Money;
using UnityEngine;
using WindowManagement;

namespace UI.Stores.ClothingShop
{
    public class ClothingShopWindow : AWindowController
    {
        [SerializeField] private ClothingShopWindowView _windowView;

        private ClothingShopWindowDataSource _dataSource;

        private void Awake()
        {
            _windowView.OnCloseWindowButtonClick += OnCloseWindowButtonClicked;
            _windowView.OnItemCellSelect += OnItemCellSelected;
        }

        private void OnCloseWindowButtonClicked()
        {
        }

        private void OnItemCellSelected(ShopItemData shopItemData)
        {
            PurchaseBroadcaster.Instance.Broadcast(
                new PurchaseItemMessage(
                    shopItemData.Item,
                    shopItemData.ShopPrice,
                    _dataSource.PlayerInventoryProvider.GetInventory()));
        }

        protected internal override void Show(IWindowIntent intent = null)
        {
            if (intent == null) throw new ArgumentNullException();
            if (!(intent is ClothingShopWindowIntent windowIntent)) throw new InvalidCastException();

            _dataSource = new ClothingShopWindowDataSource(windowIntent);

            var shopItemsDataList = new List<ShopItemData>(_dataSource.ItemsOnShop.Length);
            foreach (AItem item in _dataSource.ItemsOnShop)
            {
                if (!item.Price.IsValid()) continue;

                int currentMoney = _dataSource.PlayerInventoryProvider.GetInventory().GetItemAmount(item.Price.Type.Id);
                Price itemPurchasePrice = item.Price * windowIntent.ShopPurchasePriceMultiplier;
                shopItemsDataList.Add(new ShopItemData
                {
                    Item = item,
                    ShopPrice = itemPurchasePrice,
                    CanBuy = currentMoney >= itemPurchasePrice.Value
                });
            }

            _windowView.Setup(shopItemsDataList);
        }

        protected internal override void OnCloseWindow()
        {
        }
    }
}