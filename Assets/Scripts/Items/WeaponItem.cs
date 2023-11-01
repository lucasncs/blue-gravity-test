using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Items
{
    [CreateAssetMenu(menuName = "Item/Weapon", fileName = "WeaponItem")]
    public class WeaponItem : AItem
    {
        [SerializeField] private AssetReferenceSprite _spriteAsset;

        public AssetReferenceSprite SpriteAsset => _spriteAsset;
    }
}