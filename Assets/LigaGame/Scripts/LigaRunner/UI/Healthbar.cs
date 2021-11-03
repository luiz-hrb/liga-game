using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LigaGame.Health;

namespace LigaGame.UI
{
    public class Healthbar : MonoBehaviour
    {
        [SerializeField] private HealthBehaviour _healthBehaviour;

        private void Awake()
        {
            Subscribe();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        public void SetHealthBehaviour(HealthBehaviour healthBehaviour)
        {
            Unsubscribe();
            _healthBehaviour = healthBehaviour;
            Subscribe();
        }

        private void Subscribe()
        {
            _healthBehaviour?.OnDamage.AddListener(OnHealthChanged);
        }

        private void Unsubscribe()
        {
            _healthBehaviour?.OnDamage.RemoveListener(OnHealthChanged);
        }

        private void OnHealthChanged()
        {
            float ratio = _healthBehaviour.CurrentHealth / _healthBehaviour.MaxHealth;
            transform.localScale = new Vector3(ratio, 1, 1);
        }
    }
}
