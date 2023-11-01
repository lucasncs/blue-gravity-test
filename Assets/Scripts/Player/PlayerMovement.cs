using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _movementSpeed = 5;

        private Vector2 _movementNormalized;

        private void Update()
        {
            _movementNormalized = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _movementNormalized * _movementSpeed;
        }

        private void OnValidate()
        {
            if (TryGetComponent(out Rigidbody2D rigidbody2D))
            {
                _rigidbody = rigidbody2D;
            }
        }
    }
}