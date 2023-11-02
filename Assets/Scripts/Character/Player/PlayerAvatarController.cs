using Character.Avatar;
using Character.Player.InventoryManagement;

namespace Character.Player
{
    public class PlayerAvatarController : HumanoidCharacterAvatarController,
        IPlayerInventoryMessageListener<PlayerEquippedItemUpdatedMessage>
    {
        protected override void Awake()
        {
            base.Awake();

            PlayerInventoryBroadcaster.Instance.Subscribe(this);
        }

        protected override void OnDestroy()
        {
            PlayerInventoryBroadcaster.Instance.Unsubscribe(this);

            base.OnDestroy();
        }

        public void OnMessageReceived(PlayerEquippedItemUpdatedMessage message)
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