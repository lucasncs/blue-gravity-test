using UnityEngine;

namespace Character.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private static readonly int MOVEMENT_X = Animator.StringToHash("Movement");

        [Header("Components")]
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _animator;

        [Header("Settings")]
        [SerializeField] private float _movementSpeed = 5;

        private Vector2 _movementNormalized;

        private void Update()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            _movementNormalized = new Vector2(horizontalInput, verticalInput).normalized;

            _animator.SetFloat(MOVEMENT_X, _movementNormalized.sqrMagnitude);
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _movementNormalized * _movementSpeed;
        }

        private void OnValidate()
        {
            if (_rigidbody == null && TryGetComponent(out Rigidbody2D body2D))
            {
                _rigidbody = body2D;
            }

            if (_animator == null)
            {
                _animator = GetComponentInChildren<Animator>(true);
            }
        }
    }
}