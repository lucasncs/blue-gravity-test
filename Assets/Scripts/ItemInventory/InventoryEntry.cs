using System;
using UnityEngine;

namespace ItemInventory
{
    [Serializable]
    public struct InventoryEntry
    {
        [SerializeField] private int _id;
        [SerializeField] private int _amount;

        public int Id => _id;
        public int Amount => _amount;

        public InventoryEntry(int id, int amount)
        {
            _id = id;
            _amount = amount;
        }

        public void Increment(int quantity = 1)
        {
            _amount += quantity;
        }

        public void Decrement(int quantity = 1)
        {
            _amount -= quantity;
        }
    }
}