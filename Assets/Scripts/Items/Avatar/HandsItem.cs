using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Items.Avatar
{
    [CreateAssetMenu(menuName = "Item/Avatar/Hands", fileName = "HandsItem", order = 3)]
    public class HandsItem : AAvatarItem
    {
        [SerializeField] private AssetReferenceSprite _wristLeftSpriteAsset;
        [SerializeField] private AssetReferenceSprite _elbowLeftSpriteAsset;
        [SerializeField] private AssetReferenceSprite _wristRightSpriteAsset;
        [SerializeField] private AssetReferenceSprite _elbowRightSpriteAsset;

        public AssetReferenceSprite WristLeftSpriteAsset => _wristLeftSpriteAsset;
        public AssetReferenceSprite ElbowLeftSpriteAsset => _elbowLeftSpriteAsset;
        public AssetReferenceSprite WristRightSpriteAsset => _wristRightSpriteAsset;
        public AssetReferenceSprite ElbowRightSpriteAsset => _elbowRightSpriteAsset;
        public override ItemSlotType SlotType => ItemSlotType.Hands;
    }
}