using ItemInventory;
using Items;
using Money;
using UnityEngine;

namespace Character.Player.PurchaseManagement
{
    public class PurchaseManager : MonoBehaviour, IPurchaseMessageListener<PurchaseItemMessage>
    {
        public static PurchaseManager Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            PurchaseBroadcaster.Instance.Subscribe(this);
        }

        private void OnDestroy()
        {
            PurchaseBroadcaster.Instance.Unsubscribe(this);
        }

        public void OnMessageReceived(PurchaseItemMessage message)
        {
            ProcessPurchaseRequest(message.Item, message.Price, message.Buyer);
        }

        private void ProcessPurchaseRequest(AItem item, Price price, IReadOnlyInventory buyer, int amount = 1)
        {
            int buyerCurrencyAmount = buyer.GetItemAmount(price.Type.Id);

            if (buyerCurrencyAmount < price.Value || !(buyer is IInventory buyerInventory)) return;
            
            buyerInventory.RemoveItem(price.Type.Id, price.Value);
            buyerInventory.AddItem(item.Id, amount);
        }
    }
}