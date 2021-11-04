using System.Collections.Generic;
using UnityEngine;

namespace LigaGame.PowerUps
{
    public sealed class PowerUpTarget : MonoBehaviour
    {
        private List<PowerUpBehaviour> _currentPowerUps;

        private List<PowerUpBehaviour> CurrentPowerUpsBehaviours
        {
            get
            {
                if (_currentPowerUps == null)
                {
                    _currentPowerUps = new List<PowerUpBehaviour>();
                }
                return _currentPowerUps;
            }
        }
        
        public void EnablePowerUp(PowerUpBehaviour powerUpBehaviour)
        {
            CurrentPowerUpsBehaviours.Add(powerUpBehaviour);
            powerUpBehaviour.OnStartAction();
        }

        public void DisablePowerUp(PowerUpBehaviour powerUpBehaviour)
        {
            CurrentPowerUpsBehaviours.Remove(powerUpBehaviour);

            if (!HasPowerUpOfType(powerUpBehaviour.Type))
            {
                powerUpBehaviour.OnEndAction();
            }
        }

        private bool HasPowerUpOfType(PowerupType type)
        {
            return CurrentPowerUpsBehaviours.Find(powerUp => powerUp.Type == type) != null;
        }
    }    
}
