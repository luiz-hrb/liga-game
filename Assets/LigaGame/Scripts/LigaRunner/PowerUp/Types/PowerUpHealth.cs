using LigaGame.Health;
using UnityEngine;

namespace LigaGame.PowerUps
{
    public sealed class PowerUpHealth : PowerUpBehaviour
    {
        [SerializeField] private float _increase = 10f;

        public void Initialize(float increase)
        {
            this._increase = increase;
        }

        public override void OnStartAction()
        {
            HealthBehaviour health = Target.GetComponent<HealthBehaviour>();
            
            if (health != null)
            {
                health.ChangeHealth(_increase);
            }
        }
    }
}
