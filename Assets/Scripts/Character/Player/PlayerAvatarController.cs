using Character.Avatar;
using Character.Player.InventoryManagement;

namespace Character.Player
{
    public class PlayerAvatarController : HumanoidCharacterAvatarController,
        IPlayerInventoryMessageListener<PlayerEquippedItemUpdated>
    {
        public void OnMessageReceived(PlayerEquippedItemUpdated message)
        {
            if (message.NewEquippedItem != null)
            {
                ApplyAvatarItem(message.NewEquippedItem);
            }
            else
            {
                RemoveAvatarItem(message.SlotType);
            }
        }
    }
}