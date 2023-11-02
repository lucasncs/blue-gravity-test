using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Items.Avatar
{
    [CreateAssetMenu(menuName = "Item/Avatar/Feet", fileName = "FeetItem", order = 5)]
    public class FeetItem : AAvatarItem
    {
        [SerializeField] private AssetReferenceSprite _footLeftSpriteAsset;
        [SerializeField] private AssetReferenceSprite _footRightSpriteAsset;

        public AssetReferenceSprite FootLeftSpriteAsset => _footLeftSpriteAsset;
        public AssetReferenceSprite FootRightSpriteAsset => _footRightSpriteAsset;
        public override ItemSlotType SlotType => ItemSlotType.Feet;
    }
}