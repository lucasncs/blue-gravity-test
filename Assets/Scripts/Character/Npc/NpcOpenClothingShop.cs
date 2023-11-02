using Interaction;
using ItemInventory;
using Items;
using UI.Stores.ClothingShop;
using UnityEngine;
using WindowManagement;

namespace Character.Npc
{
    public class NpcOpenClothingShop : MonoBehaviour, IInteractable
    {
        [SerializeField] private ItemCollection _itemsOnShop;
        [SerializeField] private float _shopPurchasePriceMultiplier = 1;

        public void OpenClothingShopWindow(IInventoryProvider playerInventoryProvider)
        {
            var intent = new ClothingShopWindowIntent(
                playerInventoryProvider,
                _itemsOnShop,
                _shopPurchasePriceMultiplier);

            WindowManagementBroadcaster.Instance.Broadcast(
                new ShowWindowMessage(
                    WindowType.ClothingShop,
                    intent: intent
                ));
        }

        public void Interact(IInteractor interactor)
        {
            var inventoryProvider = interactor.GetGameObject().GetComponentInChildren<IInventoryProvider>(true);
            if (inventoryProvider == null) return;

            OpenClothingShopWindow(inventoryProvider);
        }
    }
}