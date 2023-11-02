using Interaction;
using ItemInventory;
using Items;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace WorldInteractions
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class CollectItem : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private AItem _item;
        [SerializeField] private int _quantity = 1;
        [SerializeField] private bool _destroyAfterCollecting;

        private AsyncOperationHandle<Sprite> _iconHandle;

        private void Start()
        {
            if (_iconHandle.IsValid()) Addressables.Release(_iconHandle);

            _iconHandle = Addressables.LoadAssetAsync<Sprite>(_item.Icon);
            _renderer.sprite = _iconHandle.WaitForCompletion();
        }

        private void OnDestroy()
        {
            if (_iconHandle.IsValid()) Addressables.Release(_iconHandle);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponentInChildren<IInteractor>(true) == null) return;
            if (other.GetComponentInChildren<IInventoryProvider>(true)?.GetInventory() is not IInventory inventory) return;

            inventory.AddItem(_item.Id, _quantity);

            if (_destroyAfterCollecting)
            {
                Destroy(gameObject);
            }
        }
    }
}