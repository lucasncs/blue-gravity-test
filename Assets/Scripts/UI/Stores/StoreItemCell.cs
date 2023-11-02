using System;
using Items;
using Money;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace UI.Stores
{
    public class StoreItemCell : MonoBehaviour
    {
        [SerializeField] private Button _selectButton;
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TMP_Text _itemName;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Image _priceCurrencyIcon;

        private Action<StoreItemCell> _onSelectCallback;
        private AsyncOperationHandle<Sprite> _itemIconHandle;
        private AsyncOperationHandle<Sprite> _currencyIconHandle;

        public void Setup(AItem item, Price price, bool canBuy, Action<StoreItemCell> onSelectCallback)
        {
            DisposeAllHandles();

            _itemIcon.enabled = false;
            _itemIconHandle = item.Icon.LoadAssetAsync();
            _itemIconHandle.Completed += handle => ApplySprite(_itemIcon, handle.Result);

            _priceCurrencyIcon.enabled = false;
            _currencyIconHandle = price.Type.Icon.LoadAssetAsync();
            _currencyIconHandle.Completed += handle => ApplySprite(_priceCurrencyIcon, handle.Result);

            _itemName.text = item.Name;
            _priceText.text = price.Value.ToString();

            _onSelectCallback = onSelectCallback;
            _selectButton.onClick.AddListener(OnSelectCell);

            SetCellState(canBuy);
        }

        private void ApplySprite(Image image, Sprite sprite)
        {
            image.sprite = sprite;
            image.enabled = true;
        }

        private void DisposeHandle(AsyncOperationHandle<Sprite> handle)
        {
            if (!handle.IsValid()) return;
            Addressables.Release(handle);
        }

        private void DisposeAllHandles()
        {
            DisposeHandle(_itemIconHandle);
            DisposeHandle(_currencyIconHandle);
        }

        private void OnSelectCell()
        {
            _onSelectCallback?.Invoke(this);
        }

        public void SetCellState(bool canBuy)
        {
            _selectButton.interactable = canBuy;
        }

        private void OnDestroy()
        {
            DisposeAllHandles();
        }
    }
}