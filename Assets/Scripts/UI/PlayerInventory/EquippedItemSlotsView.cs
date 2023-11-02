using System;
using System.Collections.Generic;
using Items.Avatar;
using UnityEngine;

namespace UI.PlayerInventory
{
    [Serializable]
    public class EquippedItemSlotsView
    {
        [SerializeField] private EquippedItemSlotCell _hatSlot;
        [SerializeField] private EquippedItemSlotCell _upperBodySlot;
        [SerializeField] private EquippedItemSlotCell _lowerBodySlot;
        [SerializeField] private EquippedItemSlotCell _handsSlot;
        [SerializeField] private EquippedItemSlotCell _feetSlot;
        [SerializeField] private EquippedItemSlotCell _weaponSlot;

        public event Action<ItemSlotType> OnItemSlotCellSelect;

        public void Setup(IReadOnlyDictionary<ItemSlotType, AAvatarItem> equippedItems)
        {
            _hatSlot.Setup(OnItemSlotCellSelected);
            _upperBodySlot.Setup(OnItemSlotCellSelected);
            _lowerBodySlot.Setup(OnItemSlotCellSelected);
            _handsSlot.Setup(OnItemSlotCellSelected);
            _feetSlot.Setup(OnItemSlotCellSelected);
            _weaponSlot.Setup(OnItemSlotCellSelected);

            foreach (KeyValuePair<ItemSlotType,AAvatarItem> slotItemPair in equippedItems)
            {
                ShowItem(slotItemPair.Value, slotItemPair.Key);
            }
        }

        private void OnItemSlotCellSelected(EquippedItemSlotCell cell)
        {
            if (cell.ItemInSlot == 0) return;
            OnItemSlotCellSelect?.Invoke(cell.Type);
        }

        public void ShowItem(AAvatarItem avatarItem, ItemSlotType slotType)
        {
            switch (slotType)
            {
                case ItemSlotType.Hat:
                    _hatSlot.ShowItem(avatarItem);
                    break;
                case ItemSlotType.UpperBody:
                    _upperBodySlot.ShowItem(avatarItem);
                    break;
                case ItemSlotType.Hands:
                    _handsSlot.ShowItem(avatarItem);
                    break;
                case ItemSlotType.LowerBody:
                    _lowerBodySlot.ShowItem(avatarItem);
                    break;
                case ItemSlotType.Feet:
                    _feetSlot.ShowItem(avatarItem);
                    break;
                case ItemSlotType.Holdable:
                    _weaponSlot.ShowItem(avatarItem);
                    break;
                case ItemSlotType.HeadFace:
                case ItemSlotType.Unknown:
                default:
                    return;
            }
        }
    }
}