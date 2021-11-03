using UnityEngine;
using LigaGame.Health;
using UnityEngine.Events;

namespace LigaGame.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(HealthBehaviour))]
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        [SerializeField] private float _speed = 6.0f;
        [SerializeField] private float _jumpForce = 8.0f;
        [SerializeField] private LayerMask _platformLayerMask;
        [SerializeField] private PlayerView _playerView;
        private float _targetVelocityX;
        private bool _isAlive = true;
        private bool _isGrounded;
        private Collider2D _collider;
        private Rigidbody2D _rigidbody;
        private HealthBehaviour _healthBehaviour;

        private const float _extraGoundTestHeigth = 0.1f;
        public UnityEvent OnDeath;

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }
        public float JumpForce
        {
            get => _jumpForce;
            set => _jumpForce = value;
        }

        private void Awake()
        {
            _collider = GetComponentInChildren<Collider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _healthBehaviour = GetComponent<HealthBehaviour>();

            _healthBehaviour.OnDamage.AddListener(() => Damaged());
            _healthBehaviour.OnDeath.AddListener(() => Died());
        }

        private void FixedUpdate()
        {
            Vector3 velocity = _rigidbody.velocity;
            velocity.x = _targetVelocityX;
            _rigidbody.velocity = velocity;

            _isGrounded = IsGrounded();
            _playerView.IsGrounded(_isGrounded);
        }

        public void Jump()
        {
            if (!_isAlive)
                return;
                
            if (_isGrounded)
            {
                Vector3 jumpForce = new Vector2(0f, _jumpForce);
                _rigidbody.AddForce(jumpForce);
                _playerView.Jump();
            }
        }

        public void Move(float direction)
        {
            if (!_isAlive)
                return;

            _targetVelocityX = direction * _speed;
            _playerView.Move(direction);
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

        public void Damaged()
        {
            if (!_isAlive)
                return;
                
            _playerView.Damaged();
        }

        public void Died()
        {
            if (!_isAlive)
                return;
                
            _isAlive = false;
            _playerView.Died();
            OnDeath.Invoke();
        }

        public void Revive()
        {
            _isAlive = true;
            _playerView.Revive();
        }
    }
}