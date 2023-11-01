using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 5;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            Vector3 movementNormalized = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

            _transform.position += movementNormalized * (_speed * Time.deltaTime);
        }
    }
}