using UnityEngine;

namespace Items.Avatar
{
    public abstract class AAvatarItem : AItem
    {
        public abstract ItemSlotType SlotType { get; }
    }

    public enum ItemSlotType
    {
        Unknown = -1,
        Hat,
        HeadFace,
        UpperBody,
        Hands,
        LowerBody,
        Feet,
    }
}