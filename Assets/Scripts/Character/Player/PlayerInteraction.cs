using Interaction;
using UnityEngine;

namespace Character.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private LayerMask _interactionLayer;
        [SerializeField] private KeyCode _interactionKey = KeyCode.E;
        [SerializeField] private float _interactionRadius = 5f;

        private Transform _transform;
        private bool _canInteract;
        private IInteractable _possibleInteractable;

        private void Awake()
        {
            _transform = transform;
        }

        private void FixedUpdate()
        {
            Collider2D hit = Physics2D.OverlapCircle(_transform.position, _interactionRadius, _interactionLayer);

            _canInteract = hit != null && hit.TryGetComponent(out _possibleInteractable);
        }

        private void Update()
        {
            if (!_canInteract || !Input.GetKeyDown(_interactionKey)) return;

            _possibleInteractable.Interact();
        }

        private void OnDrawGizmosSelected()
        {
#if UNITY_EDITOR
            UnityEditor.Handles.color = Color.cyan;
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.back, _interactionRadius);
#endif
        }
    }
}