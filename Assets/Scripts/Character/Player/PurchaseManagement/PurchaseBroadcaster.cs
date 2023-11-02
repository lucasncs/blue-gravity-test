using Broadcaster;
using ItemInventory;
using Items;
using Money;

namespace Character.Player.PurchaseManagement
{
    public class PurchaseBroadcaster : ABroadcaster<PurchaseBroadcaster, IPurchaseMessage>
    {
    }

    public interface IPurchaseMessageListener<T> : IMessageListener<T> where T : IPurchaseMessage
    {
    }

    public interface IPurchaseMessage : IBroadcasterMessage
    {
    }

    public struct PurchaseItemMessage : IPurchaseMessage
    {
        public readonly AItem Item;
        public readonly Price Price;
        public readonly IReadOnlyInventory Buyer;

        public PurchaseItemMessage(AItem item, Price price, IReadOnlyInventory buyer)
        {
            Item = item;
            Price = price;
            Buyer = buyer;
        }
    }
    
    // TODO: Make sell item message
}