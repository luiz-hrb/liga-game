using UnityEngine;

namespace LigaGame
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(ConstantForce2D))]
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        [SerializeField] private float _speed = 6.0f;
        [SerializeField] private float _jumpSpeed = 8.0f;
        [SerializeField] private LayerMask _platformLayerMask;
        [SerializeField] private Transform _spriteTransform;
        private Collider2D _collider;
        private Rigidbody2D _rigidbody;
        private ConstantForce2D _constantForce;
        private float _lastDirection;
        private const float _extraGoundTestHeigth = 0.01f;

        private void Awake()
        {
            _collider = GetComponentInChildren<Collider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _constantForce = GetComponent<ConstantForce2D>();
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
            Debug.Log("move");
            Vector3 movement = new Vector3(direction, 0.0f, 0.0f);
            _constantForce.force = movement * _speed;

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
            Vector2 origin = _collider.bounds.center;
            float raycastDistance = _collider.bounds.extents.y + _extraGoundTestHeigth;

            RaycastHit2D raycastHit = Physics2D.Raycast(origin, Vector2.down, raycastDistance, _platformLayerMask);
            bool isGrounded = raycastHit.collider != null;

            Color rayColor = isGrounded ? Color.green : Color.red;
            Debug.DrawRay(origin, Vector2.down * raycastDistance, rayColor);
            return isGrounded;
        }
    }
}