using WindowManagement;

namespace UI.PlayerInventory
{
    public struct PlayerInventoryWindowIntent : IWindowIntent
    {
        public readonly IPlayerInventoryWindowDataSource DataSource;

        public PlayerInventoryWindowIntent(IPlayerInventoryWindowDataSource dataSource)
        {
            DataSource = dataSource;
        }
    }
}