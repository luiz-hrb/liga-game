using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LigaGame.Health;

namespace LigaGame.Player
{
    public class FallDetectorKiller : MonoBehaviour
    {
        [SerializeField] float _minYPosition = -10f;
        [SerializeField] HealthBehaviour _target;

        public HealthBehaviour Target
        {
            get { return _target; }
            set { _target = value; }
        }

        private void Update()
        {
            CheckTargetYPosition();
        }

        private void CheckTargetYPosition()
        {
            if (Target != null && Target.CurrentHealth > 0f)
            {
                if (Target.transform.position.y < _minYPosition)
                {
                    Kill();
                }
            }
        }

        private void Kill()
        {
            if (Target != null)
            {
                Target.Damage(Target.MaxHealth);
            }
        }
    }
}
