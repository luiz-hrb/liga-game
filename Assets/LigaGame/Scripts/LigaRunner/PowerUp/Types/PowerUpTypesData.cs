using UnityEngine;

namespace LigaGame.PowerUps
{
    [CreateAssetMenu(fileName = "PowerUpTypesData", menuName = "tanks/PowerUpTypesData", order = 0)]
    public sealed class PowerUpTypesData : ScriptableObject
    {
        [SerializeField] private PowerUp[] _powerUps;

        public void Initialize(PowerUp[] powerUps)
        {
            _powerUps = powerUps;
        }

        public PowerUp[] PowerUps => _powerUps;
    }
}
