using System.Collections.Generic;
using ItemInventory;
using Items;
using Money;

namespace UI.Stores.ClothingShop
{
    public class ClothingShopWindowDataSource : IClothingShopWindowDataSource
    {
        private readonly ItemCollection _itemsOnShop;
        private readonly IInventoryProvider _playerInventoryProvider;
        private readonly ItemDatabase _itemDatabase;
        private readonly float _shopPurchasePriceMultiplier;

        public ClothingShopWindowDataSource(
            IInventoryProvider playerInventoryProvider,
            ItemCollection itemsOnShop,
            float shopPurchasePriceMultiplier,
            ItemDatabase itemDatabase)
        {
            _playerInventoryProvider = playerInventoryProvider;
            _itemsOnShop = itemsOnShop;
            _shopPurchasePriceMultiplier = shopPurchasePriceMultiplier;
            _itemDatabase = itemDatabase;
        }

        public IReadOnlyInventory GetInventory()
        {
            return _playerInventoryProvider.GetInventory();
        }

        public ItemCollection GetAllItemsOnShop()
        {
            return _itemsOnShop;
        }

        public IEnumerable<AItem> GetAllItems()
        {
            foreach (InventoryEntry entry in _playerInventoryProvider.GetInventory().GetAllItems())
            {
                AItem item = _itemDatabase.GetItem(entry.Id);
                if (item is CurrencyItem) continue;
                yield return item;
            }
        }

        public Price CalculateSellingPrice(Price basePrice)
        {
            return basePrice * _shopPurchasePriceMultiplier;
        }
    }
}