using UnityEngine;

namespace LigaGame
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        [SerializeField] private float _speed = 6.0f;
        [SerializeField] private float _jumpForce = 8.0f;
        [SerializeField] private LayerMask _platformLayerMask;
        [SerializeField] private Transform _spriteTransform;
        private float _targetVelocityX;
        private Collider2D _collider;
        private Rigidbody2D _rigidbody;
        private float _lastDirection;
        private const float _extraGoundTestHeigth = 0.01f;

        private void Awake()
        {
            _collider = GetComponentInChildren<Collider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Vector3 velocity = _rigidbody.velocity;
            velocity.x = _targetVelocityX;
            _rigidbody.velocity = velocity;
        }

        public void Jump()
        {
            if (IsGrounded())
            {
                Vector3 jumpForce = new Vector2(0f, _jumpForce);
                _rigidbody.AddForce(jumpForce);
            }
        }

        public void Move(float direction)
        {
            // Debug.Log("move");
            _targetVelocityX = direction * _speed;

            bool willChangeDirection = direction != 0f;

            if (willChangeDirection)
            {
                bool willFaceRight = direction > 0.0f;
                _spriteTransform.localScale = new Vector3(willFaceRight ? 1.0f : -1.0f, 1.0f, 1.0f);
            }

            _lastDirection = direction;
        }

        private bool IsGrounded()
        {
            return true;
            Vector2 origin = _collider.bounds.center;
            float raycastDistance = _collider.bounds.extents.y + _extraGoundTestHeigth;

            RaycastHit2D raycastHit = Physics2D.Raycast(origin, Vector2.down, raycastDistance, _platformLayerMask);
            bool isGrounded = raycastHit.collider != null;

            Color rayColor = isGrounded ? Color.green : Color.red;
            Debug.DrawRay(origin, Vector2.down * raycastDistance, rayColor);
            return isGrounded;
        }

        public void Die()
        {

        }

        public void Revive()
        {

        }
    }
}