using System;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace UI.PlayerInventory
{
    public class InventoryItemCell : MonoBehaviour
    {
        [SerializeField] private Button _selectButton;
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TMP_Text _itemName;
        [SerializeField] private TMP_Text _itemAmount;

        private Action<InventoryItemCell> _onSelectCallback;
        private AsyncOperationHandle<Sprite> _itemIconHandle;

        public void Setup(AItem item, int amount, Action<InventoryItemCell> onSelectCallback)
        {
            DisposeHandle(_itemIconHandle);

            _itemIcon.enabled = false;
            _itemIconHandle = Addressables.LoadAssetAsync<Sprite>(item.Icon);
            _itemIconHandle.Completed += handle => ApplySprite(_itemIcon, handle.Result);

            _itemName.text = item.Name;
            _itemAmount.text = amount.ToString();

            _onSelectCallback = onSelectCallback;
            _selectButton.onClick.AddListener(OnSelectCell);
        }

        private void DisposeHandle(AsyncOperationHandle<Sprite> handle)
        {
            if (!handle.IsValid()) return;
            Addressables.Release(handle);
        }

        private void ApplySprite(Image image, Sprite sprite)
        {
            image.sprite = sprite;
            image.enabled = true;
        }

        private void OnSelectCell()
        {
            _onSelectCallback?.Invoke(this);
        }

        private void OnDestroy()
        {
            DisposeHandle(_itemIconHandle);
        }
    }
}