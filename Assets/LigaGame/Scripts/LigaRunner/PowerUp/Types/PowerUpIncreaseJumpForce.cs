using UnityEngine;
using LigaGame.Player;

namespace LigaGame.PowerUps
{
    public sealed class PowerUpIncreaseJumpForce : PowerUpBehaviour
    {
        [SerializeField] private float _jumpForce = 600f;
        private PlayerController _player;

        public override void OnStartAction()
        {
            _player = Target.GetComponent<PlayerController>();
            
            if (_player == null)
            {
                return;
            }

            _player.JumpForce = _jumpForce;
        }

        public override void OnEndAction()
        {
            if (_player == null)
            {
                return;
            }

            _player.ResetJumpForce();
        }
    }
}
