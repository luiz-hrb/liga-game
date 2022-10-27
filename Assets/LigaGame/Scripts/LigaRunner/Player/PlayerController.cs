using UnityEngine;
using LigaGame.Health;
using UnityEngine.Events;

namespace LigaGame.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(HealthBehaviour))]
    public class PlayerController : MonoBehaviour, IPlayer
    {
        [SerializeField] private float _speed = 6.0f;
        [SerializeField] private float _jumpForce = 8.0f;
        [SerializeField] private LayerMask _platformLayerMask;
        [SerializeField] private PlayerView _playerView;
        private float _targetVelocityX;
        private bool _isAlive = true;
        private bool _canInteract = true;
        private bool _isGrounded;
        private bool _doubleJumped;
        private Collider2D _collider;
        private Rigidbody2D _rigidbody;
        private HealthBehaviour _healthBehaviour;

        public HealthBehaviour HealthBehaviour =>_healthBehaviour ?? (_healthBehaviour = GetComponent<HealthBehaviour>());

        private const float _extraGoundTestHeigth = 0.1f;
        public UnityEvent OnDeath;
        private float _defaultJumpForce;

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
        public bool CanInteract
        {
            get => _canInteract && _isAlive;
            set
            {
                Move(0f);
                _canInteract = value;
            }
        }

        private void Awake()
        {
            _collider = GetComponentInChildren<Collider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _healthBehaviour = GetComponent<HealthBehaviour>();

            _healthBehaviour.OnHealthChanged.AddListener(HealthChanged);
            _healthBehaviour.OnDeath.AddListener(Died);

            _defaultJumpForce = _jumpForce;
        }

        internal void ResetJumpForce()
        {
            JumpForce = _defaultJumpForce;
        }

        private void FixedUpdate()
        {
            ApplyVelocity();

            _isGrounded = IsGrounded();
            _playerView.IsGrounded(_isGrounded);
            
            if (_isGrounded && _doubleJumped)
            {
                _doubleJumped = false;
            }
        }

        private void ApplyVelocity()
        {
            Vector2 velocity = _rigidbody.velocity;
            velocity.x = _targetVelocityX;
            _rigidbody.velocity = velocity;
        }

        public void Move(float direction)
        {
            if (!CanInteract)
                return;

            _targetVelocityX = direction * _speed;
            _playerView.Move(direction);
        }

        public void Jump()
        {
            if (!CanInteract)
                return;
                
            bool canJump = _isGrounded;
            if (!_isGrounded)
            {
                if (!_doubleJumped)
                {
                    _doubleJumped = true;
                    canJump = true;
                }
            }

            if (canJump)
            {
                ApplyJumpForce();
                _playerView.Jump();
            }
        }

        private void ApplyJumpForce()
        {
            Vector3 velocity = _rigidbody.velocity;
            velocity.y = _jumpForce;
            _rigidbody.velocity = velocity;
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

        public void HealthChanged(float healthChange)
        {
            if (!_isAlive)
                return;

            _playerView.HealthChanged(healthChange);
        }

        public void Died()
        {
            if (!_isAlive)
                return;
                
            _targetVelocityX = 0f;
            _isAlive = false;
            _playerView.Died();
            OnDeath.Invoke();
        }
    }
}