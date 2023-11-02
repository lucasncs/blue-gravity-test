namespace ItemInventory
{
    public interface IInventoryProvider
    {
        IReadOnlyInventory GetInventory();
    }
}