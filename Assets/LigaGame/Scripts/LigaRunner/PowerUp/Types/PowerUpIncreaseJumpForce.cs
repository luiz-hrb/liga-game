using UnityEngine;
using LigaGame.Player;

namespace LigaGame.PowerUps
{
    public sealed class PowerUpIncreaseJumpForce : PowerUpBehaviour
    {
        [SerializeField] private float _jumpForce = 600f;
        private float _jumpForceDefault;
        private PlayerController _player;

        public override void OnStartAction()
        {
            _player = Target.GetComponent<PlayerController>();
            
            if (_player == null)
            {
                return;
            }

            _jumpForceDefault = _player.JumpForce;
            _player.JumpForce = _jumpForce;
        }

        public override void OnEndAction()
        {
            if (_player == null)
            {
                return;
            }

            _player.JumpForce = _jumpForceDefault;
        }
    }
}
