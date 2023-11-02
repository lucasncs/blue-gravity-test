using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Items.Avatar
{
    [CreateAssetMenu(menuName = "Item/Avatar/Hat", fileName = "HatItem", order = 0)]
    public class HatItem : AAvatarItem
    {
        [SerializeField] private AssetReferenceSprite _spriteAsset;

        public AssetReferenceSprite SpriteAsset => _spriteAsset;
        public override ItemSlotType SlotType => ItemSlotType.Hat;
    }
}