using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Items
{
    public abstract class AItem : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private AssetReferenceSprite _icon;
        [SerializeField] private string _name;

        public int Id => _id;
        public AssetReferenceSprite Icon => _icon;
        public string Name => _name;
    }
}