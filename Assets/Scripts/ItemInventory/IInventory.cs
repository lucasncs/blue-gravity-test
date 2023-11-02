using System.Collections.Generic;

namespace ItemInventory
{
    public interface IInventory : IReadOnlyInventory
    {
        void AddItems(IEnumerable<InventoryEntry> items);

        void AddItem(int id, int quantity = 1);

        void RemoveItem(int id, int quantity = 1);
    }

    public interface IReadOnlyInventory : IEnumerable<InventoryEntry>
    {
        bool HasItem(int id);

        bool HasAnyItem(IEnumerable<int> items);

        bool HasAllItems(IEnumerable<int> items);

        int GetItemAmount(int id);
    }
}