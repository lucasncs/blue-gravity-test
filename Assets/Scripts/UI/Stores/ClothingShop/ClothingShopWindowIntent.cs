using ItemInventory;
using Items;
using WindowManagement;

namespace UI.Stores.ClothingShop
{
    public struct ClothingShopWindowIntent : IWindowIntent
    {
        public readonly IInventoryProvider PlayerInventoryProvider;
        public readonly ItemCollection ItemsOnShop;
        public readonly float ShopPurchasePriceMultiplier;

        public ClothingShopWindowIntent(
            IInventoryProvider playerInventoryProvider,
            ItemCollection itemsOnShop,
            float shopPurchasePriceMultiplier)
        {
            PlayerInventoryProvider = playerInventoryProvider;
            ItemsOnShop = itemsOnShop;
            ShopPurchasePriceMultiplier = shopPurchasePriceMultiplier;
        }
    }
}