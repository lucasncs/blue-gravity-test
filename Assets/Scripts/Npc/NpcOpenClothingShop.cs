using UnityEngine;
using WindowManagement;

namespace Npc
{
    public class NpcOpenClothingShop : MonoBehaviour
    {
        public void OpenClothingShopWindow()
        {
            WindowManagementBroadcaster.Instance.Broadcast(new ShowWindowMessage(WindowType.ClothingShop));
        }
    }
}