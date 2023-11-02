using WindowManagement;

namespace UI.Stores.ClothingShop
{
    public struct ClothingShopWindowIntent : IWindowIntent
    {
        public readonly IClothingShopWindowDataSource DataSource;

        public ClothingShopWindowIntent(IClothingShopWindowDataSource dataSource)
        {
            DataSource = dataSource;
        }
    }
}