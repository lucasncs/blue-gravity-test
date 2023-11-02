using System;
using Items.Avatar;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace UI.PlayerInventory
{
    public class EquippedItemSlotCell : MonoBehaviour
    {
        [SerializeField] private ItemSlotType _type;
        [SerializeField] private Image _icon;
        [SerializeField] private Button _selectButton;

        private Action<EquippedItemSlotCell> _onSelectCallback;
        private AsyncOperationHandle<Sprite> _iconHandle;

        public int ItemInSlot { get; private set; }
        public ItemSlotType Type => _type;

        public void Setup(Action<EquippedItemSlotCell> onSelectCallback)
        {
            _onSelectCallback = onSelectCallback;
            _selectButton.onClick.AddListener(OnSelectCell);
        }

        private void OnSelectCell()
        {
            _onSelectCallback?.Invoke(this);
        }

        public void ShowItem(AAvatarItem item)
        {
            ItemInSlot = 0;
            if (item == null)
            {
                ReleaseHandle();
                _icon.enabled = false;
                return;
            }

            if (item.SlotType != _type) return;
            ReleaseHandle();

            ItemInSlot = item.Id;

            _icon.enabled = false;
            _iconHandle = Addressables.LoadAssetAsync<Sprite>(item.Icon);
            _icon.sprite = _iconHandle.WaitForCompletion();
            _icon.enabled = true;
        }

        private void ReleaseHandle()
        {
            if (_iconHandle.IsValid()) Addressables.Release(_iconHandle);
        }

        private void OnDestroy()
        {
            ReleaseHandle();
        }
    }
}