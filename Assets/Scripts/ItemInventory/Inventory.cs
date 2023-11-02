using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ItemInventory
{
    public class Inventory : IInventory
    {
        private readonly Dictionary<int, InventoryEntry> _items = new();

        public event Action<InventoryEntry> OnItemAdded;
        public event Action<InventoryEntry> OnItemRemoved;

        public void Initialize(IEnumerable<int> items)
        {
            if (items == null) return;

            var enumerator = items.GetEnumerator();
            enumerator.Reset();
            enumerator.MoveNext();
            while (true)
            {
                int id = enumerator.Current;
                enumerator.MoveNext();
                int quantity = enumerator.Current;

                QuietlyAddItem(id, quantity);

                if (!enumerator.MoveNext()) break;
            }

            enumerator.Dispose();
        }

        public void AddItems(IEnumerable<InventoryEntry> items)
        {
            if (items == null) return;

            foreach (InventoryEntry item in items)
            {
                AddItem(item.Id, item.Amount);
            }
        }

        public void AddItem(int id, int quantity = 1)
        {
            InventoryEntry item = QuietlyAddItem(id, quantity);

            OnItemAdded?.Invoke(item);
        }

        private InventoryEntry QuietlyAddItem(int id, int quantity)
        {
            if (!_items.TryGetValue(id, out InventoryEntry item))
            {
                return _items[id] = new InventoryEntry(id, quantity);
            }

            item.Increment(quantity);
            return _items[id] = item;
        }

        public void RemoveItem(int id, int quantity = 1)
        {
            if (!_items.TryGetValue(id, out InventoryEntry item)) return;
            item.Decrement(quantity);
            _items[id] = item;

            if (item.Amount < 1) _items.Remove(id);

            OnItemRemoved?.Invoke(item);
        }

        public bool HasItem(int id)
        {
            return _items.ContainsKey(id);
        }

        public bool HasAnyItem(IEnumerable<int> items)
        {
            return items.Any(item => _items.ContainsKey(item));
        }

        public bool HasAllItems(IEnumerable<int> items)
        {
            return items.All(item => _items.ContainsKey(item));
        }

        public int GetItemAmount(int id)
        {
            return _items.TryGetValue(id, out InventoryEntry item) ? item.Amount : 0;
        }

        public IEnumerable<InventoryEntry> GetAllItems()
        {
            return _items.Values.AsEnumerable();
        }

        public IEnumerator<InventoryEntry> GetEnumerator()
        {
            return _items.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}