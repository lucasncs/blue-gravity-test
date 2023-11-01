using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Items.Avatar
{
    [CreateAssetMenu(menuName = "Item/Avatar/Lower Body", fileName = "LowerBodyItem")]
    public class LowerBodyItem : AAvatarItem
    {
        [SerializeField] private AssetReferenceSprite _pelvisSpriteAsset;
        [SerializeField] private AssetReferenceSprite _legLeftSpriteAsset;
        [SerializeField] private AssetReferenceSprite _legRightSpriteAsset;

        public AssetReferenceSprite PelvisSpriteAsset => _pelvisSpriteAsset;
        public AssetReferenceSprite LegLeftSpriteAsset => _legLeftSpriteAsset;
        public AssetReferenceSprite LegRightSpriteAsset => _legRightSpriteAsset;
        public override ItemSlotType SlotType => ItemSlotType.LowerBody;
    }
}