using Broadcaster;
using Items;
using Items.Avatar;

namespace Character.Player.InventoryManagement
{
    public class PlayerInventoryBroadcaster : ABroadcaster<PlayerInventoryBroadcaster, IPlayerInventoryMessage>
    {
    }

    public interface IPlayerInventoryMessageListener<T> : IMessageListener<T> where T : IPlayerInventoryMessage
    {
    }

    public interface IPlayerInventoryMessage : IBroadcasterMessage
    {
    }

    public struct PlayerInventoryUpdateMessage : IPlayerInventoryMessage
    {
    }

    public struct PlayerChangeEquippedItem : IPlayerInventoryMessage
    {
        public readonly ItemSlotType SlotToChange;
        public readonly int NewEquippedItemId;

        public PlayerChangeEquippedItem(ItemSlotType slotToChange, int newEquippedItemId)
        {
            SlotToChange = slotToChange;
            NewEquippedItemId = newEquippedItemId;
        }
    }

    public struct PlayerEquippedItemUpdated : IPlayerInventoryMessage
    {
        public readonly ItemSlotType SlotType;
        public readonly AAvatarItem NewEquippedItem;

        public PlayerEquippedItemUpdated(ItemSlotType slotType, AAvatarItem newEquippedItem)
        {
            NewEquippedItem = newEquippedItem;
            SlotType = slotType;
        }
    }

    public struct PlayerCurrencyUpdateMessage : IPlayerInventoryMessage
    {
        public readonly CurrencyItem Currency;
        public readonly int CurrentAmount;

        public PlayerCurrencyUpdateMessage(CurrencyItem currency, int currentAmount)
        {
            Currency = currency;
            CurrentAmount = currentAmount;
        }
    }
}