using Broadcaster;

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
}