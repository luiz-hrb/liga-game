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

        public UnityEvent OnDamage;
        public UnityEvent OnHeal;
        public UnityEvent OnDeath;

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        public void Damage(float damage)
        {
            float newHealth = _currentHealth - damage;
            newHealth = Mathf.Clamp(newHealth, 0f, _maxHealth);
            SetHealth(newHealth);
        }

        private void SetHealth(float health)
        {
            bool isDamage = health < _currentHealth;
            _currentHealth = health;

            if (isDamage)
            {
                OnDamage.Invoke();
            }
            else
            {
                OnHeal.Invoke();
            }

            if (!IsAlive)
            {
                OnDeath.Invoke();
            }
        }
    }
}
