namespace Items.Avatar
{
    public abstract class AHoldableItem : AAvatarItem
    {
        public override ItemSlotType SlotType => ItemSlotType.Holdable;
    }
}