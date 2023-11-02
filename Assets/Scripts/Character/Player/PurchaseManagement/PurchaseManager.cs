using ItemInventory;
using Items;
using Money;
using UnityEngine;

namespace Character.Player.PurchaseManagement
{
    public class PurchaseManager : MonoBehaviour,
        IPurchaseMessageListener<PurchaseItemMessage>,
        IPurchaseMessageListener<SellItemMessage>
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
            PurchaseBroadcaster.Instance.Subscribe<PurchaseItemMessage>(this);
        }

        private void OnDestroy()
        {
            PurchaseBroadcaster.Instance.Unsubscribe<PurchaseItemMessage>(this);
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

        public void OnMessageReceived(SellItemMessage message)
        {
            ProcessSellRequest(message.Item, message.Item.Price, message.Seller);
        }

        private void ProcessSellRequest(AItem item, Price price, IReadOnlyInventory seller, int amount = 1)
        {
            if (!(seller is IInventory sellerInventory)) return;

            sellerInventory.AddItem(price.Type.Id, price.Value);
            sellerInventory.RemoveItem(item.Id, amount);
        }
    }
}