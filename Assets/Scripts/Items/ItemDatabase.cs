using UnityEngine;

namespace Items
{
    // Note: Probably in a real game, this would be accessed through some service. Just using the reference like this as a way to simplify things, so bear with me.
    public class ItemDatabase : MonoBehaviour
    {
        public static ItemCollection Database { get; private set; }

        [SerializeField] private ItemCollection _database;

        private void Awake()
        {
            if (Database == null)
            {
                Database = _database;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}