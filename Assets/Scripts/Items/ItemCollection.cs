using AssetCollection;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Item/Item Collection", fileName = "ItemCollection", order = 100)]
    public class ItemCollection : AssetCollection<AItem>
    {
    }
}