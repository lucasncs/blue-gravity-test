using ItemInventory;
using Items;

namespace UI.Stores.ClothingShop
{
    public class ClothingShopWindowDataSource
    {
        public readonly IInventoryProvider PlayerInventoryProvider;
        public readonly ItemCollection ItemsOnShop;

        public ClothingShopWindowDataSource(ClothingShopWindowIntent windowIntent)
        {
            PlayerInventoryProvider = windowIntent.PlayerInventoryProvider;
            ItemsOnShop = windowIntent.ItemsOnShop;
        }
    }
}