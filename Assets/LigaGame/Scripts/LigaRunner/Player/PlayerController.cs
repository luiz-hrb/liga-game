using UnityEngine;

namespace LigaGame
{
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        [SerializeField] private float _speed = 6.0f;
        [SerializeField] private float _jumpSpeed = 8.0f;
        [SerializeField] private LayerMask _platformLayerMask;
        [SerializeField] private Transform _spriteTransform;
        private Collider2D _collider;
        private Rigidbody _rigidbody;
        private float _lastDirection;
        private const float _extraGoundTestHeigth = 0.01f;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Jump()
        {
            if (IsGrounded())
            {
                Vector3 jump = new Vector3(0.0f, _jumpSpeed, 0.0f);
                GetComponent<Rigidbody>().AddForce(jump);
            }
        }

        public void Move(float direction)
        {
            Vector3 movement = new Vector3(direction, 0.0f, 0.0f);
            _rigidbody.velocity = movement * _speed;
            bool invertDirection = direction * _lastDirection < 0.0f;
            if (invertDirection)
            {
                bool willFaceRight = direction > 0.0f;
                _spriteTransform.localScale = new Vector3(willFaceRight ? 1.0f : -1.0f, 1.0f, 1.0f);
            }

            _lastDirection = direction;
        }

        private bool IsGrounded()
        {
            Vector2 origin = _collider.bounds.center;
            float raycastDistance = _collider.bounds.extents.y + _extraGoundTestHeigth;

            RaycastHit2D raycastHit = Physics2D.Raycast(origin, Vector2.down, raycastDistance, _platformLayerMask);
            bool isGrounded = raycastHit.collider != null;

            Color rayColor = isGrounded ? Color.green : Color.red;
            return isGrounded;
        }
    }
}