using Items.Avatar;
using UnityEngine;

namespace Character.Avatar
{
    public abstract class ACharacterAvatarController : MonoBehaviour
    {
        public abstract void ApplyAvatarItem(AAvatarItem avatarItem);
    }
}