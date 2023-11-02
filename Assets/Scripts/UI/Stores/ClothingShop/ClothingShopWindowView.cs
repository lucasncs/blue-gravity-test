using System;
using System.Collections.Generic;
using Character.Player.InventoryManagement;
using UnityEngine;
using UnityEngine.UI;
using WindowManagement;

namespace UI.Stores.ClothingShop
{
    public class ClothingShopWindowView : MonoBehaviour,
        IPlayerInventoryMessageListener<PlayerCurrencyUpdateMessage>
    {
        [SerializeField] private StoreItemCell _itemCellPrefab;
        [SerializeField] private RectTransform _itemCellsParent;
        [SerializeField] private Button _closeWindowButton;

        private readonly Dictionary<StoreItemCell, ShopItemData> _storeItemCells = new();

        public event Action OnCloseWindowButtonClick;
        public event Action<ShopItemData> OnItemCellSelect;

        private void Awake()
        {
            _closeWindowButton.onClick.AddListener(OnCloseWindowButtonClicked);
            PlayerInventoryBroadcaster.Instance.Subscribe(this);
        }

        private void OnDestroy()
        {
            PlayerInventoryBroadcaster.Instance.Unsubscribe(this);
        }

        private void OnCloseWindowButtonClicked()
        {
            OnCloseWindowButtonClick?.Invoke();
            WindowManagementBroadcaster.Instance.Broadcast(new CloseWindowMessage());
        }

        public void Setup(IEnumerable<ShopItemData> itemsOnShop)
        {
            PopulateItemsOnShop(itemsOnShop);
        }

        private void PopulateItemsOnShop(IEnumerable<ShopItemData> itemsOnShop)
        {
            for (int i = _itemCellsParent.childCount - 1; i >= 0; i--)
            {
                Destroy(_itemCellsParent.GetChild(i).gameObject);
            }

            _storeItemCells.Clear();

            foreach (ShopItemData shopItemData in itemsOnShop)
            {
                StoreItemCell cell = Instantiate(_itemCellPrefab, _itemCellsParent);
                cell.Setup(shopItemData.Item, shopItemData.ShopPrice, shopItemData.CanBuy, OnItemCellSelected);

                _storeItemCells[cell] = shopItemData;
            }
        }

        private void OnItemCellSelected(StoreItemCell cell)
        {
            OnItemCellSelect?.Invoke(_storeItemCells[cell]);
        }

        public void OnMessageReceived(PlayerCurrencyUpdateMessage message)
        {
            foreach (KeyValuePair<StoreItemCell, ShopItemData> cellPair in _storeItemCells)
            {
                if (cellPair.Value.ShopPrice.Type.Id != message.Currency.Id) continue;

                cellPair.Key.SetCellState(message.CurrentAmount >= cellPair.Value.ShopPrice.Value);
            }
        }
    }
}