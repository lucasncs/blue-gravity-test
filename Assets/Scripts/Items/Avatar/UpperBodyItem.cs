using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Items.Avatar
{
    [CreateAssetMenu(menuName = "Item/Avatar/Upper Body", fileName = "UpperBodyItem", order = 2)]
    public class UpperBodyItem : AAvatarItem
    {
        [SerializeField] private AssetReferenceSprite _torsoSpriteAsset;
        [SerializeField] private AssetReferenceSprite _shoulderLeftSpriteAsset;
        [SerializeField] private AssetReferenceSprite _shoulderRightSpriteAsset;

        public AssetReferenceSprite TorsoSpriteAsset => _torsoSpriteAsset;
        public AssetReferenceSprite ShoulderLeftSpriteAsset => _shoulderLeftSpriteAsset;
        public AssetReferenceSprite ShoulderRightSpriteAsset => _shoulderRightSpriteAsset;
        public override ItemSlotType SlotType => ItemSlotType.UpperBody;
    }
}