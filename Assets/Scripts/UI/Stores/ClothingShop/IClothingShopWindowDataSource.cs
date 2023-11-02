using System.Collections.Generic;
using ItemInventory;
using Items;
using Money;

namespace UI.Stores.ClothingShop
{
    public interface IClothingShopWindowDataSource : IInventoryProvider
    {
        ItemCollection GetAllItemsOnShop();
        IEnumerable<AItem> GetAllItems();
        Price CalculateSellingPrice(Price basePrice);
    }
}