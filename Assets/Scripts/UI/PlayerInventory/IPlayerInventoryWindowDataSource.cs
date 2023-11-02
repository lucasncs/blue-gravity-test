using System.Collections.Generic;
using Character.Player.InventoryManagement;
using ItemInventory;
using Items;
using Items.Avatar;

namespace UI.PlayerInventory
{
    public interface IPlayerInventoryWindowDataSource : IInventoryProvider, IEquippedItems
    {
        IEnumerable<InventoryListEntry> GetAllItems();
        IReadOnlyDictionary<ItemSlotType, AAvatarItem> GetEquippedItems();
        AItem GetItem(int id);
    }
}