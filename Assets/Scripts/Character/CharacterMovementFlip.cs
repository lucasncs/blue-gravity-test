using UnityEngine;

namespace Character
{
    public class CharacterMovementFlip : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        private Transform _transform;
        private bool _isFacingRight = true;

        private void Awake()
        {
            _transform = transform;
        }

        private void LateUpdate()
        {
            float velocityX = _rigidbody.velocity.x;
            if (Mathf.Approximately(velocityX, 0)) return;

            if ((velocityX > 0 && !_isFacingRight) ||
                (velocityX < 0 && _isFacingRight)) Flip();
        }

        private void Flip()
        {
            Vector3 currentScale = _transform.localScale;
            currentScale.x *= -1;
            _transform.localScale = currentScale;

            _isFacingRight = !_isFacingRight;
        }

        private void OnValidate()
        {
            if (_rigidbody == null && TryGetComponent(out Rigidbody2D body2D))
            {
                _rigidbody = body2D;
            }
        }
    }
}