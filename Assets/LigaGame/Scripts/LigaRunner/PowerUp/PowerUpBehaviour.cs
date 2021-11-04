using UnityEngine;

namespace LigaGame.PowerUps
{
    public class PowerUpBehaviour : MonoBehaviour
    {        
        [SerializeField] private PowerupType _type;
        private PowerUpTarget _target;

        public PowerupType Type { get => _type; set => _type = value; }
        public PowerUpTarget Target { get => _target; set => _target = value; }

        public void Initialize(PowerUpTarget target)
        {
            _target = target;
        }

        public virtual void OnStartAction() { }
        public virtual void OnEndAction() { }
    }
}
