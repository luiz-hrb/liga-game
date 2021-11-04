using UnityEngine;
using UnityEngine.Events;

namespace LigaGame.Health
{
    public sealed class HealthBehaviour : MonoBehaviour
    {
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private float _currentHealth;
        
        public float MaxHealth => _maxHealth;
        public float CurrentHealth => _currentHealth;
        public bool IsAlive => _currentHealth > 0f;

        public UnityEvent<float> OnHealthChanged;
        public UnityEvent OnDeath;

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        public void ChangeHealth(float change)
        {
            SetHealth(_currentHealth + change);
            OnHealthChanged.Invoke(change);
        }

        private void SetHealth(float health)
        {
            _currentHealth = Mathf.Clamp(health, 0f, _maxHealth);

            if (!IsAlive)
            {
                OnDeath.Invoke();
            }
        }
    }
}
