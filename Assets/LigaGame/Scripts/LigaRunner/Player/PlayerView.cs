using UnityEngine;

namespace LigaGame.Player
{
    public class PlayerView : MonoBehaviour, IPlayer
    {
        [SerializeField] Animator _animator;
        [SerializeField] private Transform _spriteTransform;

        public void Move(float direction)
        {
            bool isRunning = direction != 0;
            _animator.SetBool("IsRunning", isRunning);

            if (isRunning)
            {
                bool willFaceRightDirection = direction > 0.0f;
                _spriteTransform.localScale = new Vector3(willFaceRightDirection ? 1.0f : -1.0f, 1.0f, 1.0f);
            }
        }

        public void Jump()
        {
            _animator.SetTrigger("Jump");
        }

        public void HealthChanged(float healthChange)
        {
            if (healthChange < 0f)
            {
                _animator.SetTrigger("Damaged");
            }
        }

        public void Died()
        {
            _animator.SetTrigger("Died");
        }

        public void IsGrounded(bool isGrounded)
        {
            _animator.SetBool("IsGrounded", isGrounded);
        }
    }
}
