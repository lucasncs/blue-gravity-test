using System.Collections.Generic;
using ItemInventory;
using Items.Avatar;
using UnityEngine;

namespace Character.Player.InventoryManagement
{
    public class PlayerInventory : MonoBehaviour, IInventoryProvider
    {
        private readonly Inventory _inventory = new();

        private readonly Dictionary<ItemSlotType, int> _equippedItems = new()
        {
            { ItemSlotType.Hat, 0 },
            { ItemSlotType.HeadFace, 0 },
            { ItemSlotType.UpperBody, 0 },
            { ItemSlotType.Hands, 0 },
            { ItemSlotType.LowerBody, 0 },
            { ItemSlotType.Feet, 0 },
            { ItemSlotType.Holdable, 0 }
        };

        private void Start()
        {
            _inventory.OnItemAdded += OnItemAdded;
            _inventory.OnItemRemoved += OnItemRemoved;
        }

        private void OnItemAdded(InventoryEntry item)
        {
            // TODO: Broadcast message
        }

        private void OnItemRemoved(InventoryEntry item)
        {
            foreach (KeyValuePair<ItemSlotType,int> slotItemPair in _equippedItems)
            {
                if (slotItemPair.Value == item.Id)
                {
                    _equippedItems[slotItemPair.Key] = 0;
                    break;
                }
            }
        }

        private void OnDestroy()
        {
            _inventory.OnItemAdded -= OnItemAdded;
            _inventory.OnItemRemoved -= OnItemRemoved;
        }

        public IReadOnlyInventory GetInventory()
        {
            return _inventory;
        }
    }
}