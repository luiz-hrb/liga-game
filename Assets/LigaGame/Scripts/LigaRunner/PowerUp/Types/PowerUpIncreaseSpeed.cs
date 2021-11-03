using UnityEngine;
using LigaGame.Player;

namespace LigaGame.PowerUp
{
    public sealed class PowerUpIncreaseSpeed : PowerUpBehaviour
    {
        [SerializeField] private float _velocity = 8f;
        private float _velocityDefault;
        private PlayerController _player;

        public override void OnStartAction()
        {
            _player = Target.GetComponent<PlayerController>();
            
            if (_player == null)
            {
                return;
            }

            _velocityDefault = _player.Speed;
            _player.Speed = _velocity;
        }

        public override void OnEndAction()
        {
            if (_player == null)
            {
                return;
            }

            _player.Speed = _velocityDefault;
        }
    }
}
