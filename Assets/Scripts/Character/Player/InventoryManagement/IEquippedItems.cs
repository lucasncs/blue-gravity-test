using System.Collections.Generic;
using Items.Avatar;

namespace Character.Player.InventoryManagement
{
    public interface IEquippedItems
    {
        int GetEquippedItem(ItemSlotType slotType);
        IEnumerable<int> GetAllEquippedItems();
    }
}