using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Items.Avatar
{
    [CreateAssetMenu(menuName = "Item/Avatar/Head Face", fileName = "HeadFaceItem")]
    public class HeadFaceItem : AAvatarItem
    {
        [SerializeField] private AssetReferenceSprite _spriteAsset;

        public AssetReferenceSprite SpriteAsset => _spriteAsset;
        public override ItemSlotType SlotType => ItemSlotType.HeadFace;
    }
}