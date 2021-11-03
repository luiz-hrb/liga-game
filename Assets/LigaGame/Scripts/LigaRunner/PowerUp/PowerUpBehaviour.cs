using UnityEngine;

namespace LigaGame.PowerUp
{
    public class PowerUpBehaviour : MonoBehaviour
    {
        public enum Type { IncreaseSpeed, IncreaseJumpForce, RecoverHealth }
        
        [SerializeField] private Type _type;
        private PowerUpTarget _target;

        public Type _Type { get => _type; set => _type = value; }
        public PowerUpTarget Target { get => _target; set => _target = value; }

        public void Initialize(PowerUpTarget target)
        {
            _target = target;
        }

        public virtual void OnStartAction() { }
        public virtual void OnEndAction() { }
    }
}
