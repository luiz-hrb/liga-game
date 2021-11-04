using UnityEngine;
using LigaGame.Health;

namespace LigaGame.UI
{
    public class Healthbar : MonoBehaviour
    {
        [SerializeField] private float _lerp = 2f;
        [SerializeField] private HealthBehaviour _healthBehaviour;
        [SerializeField] private Transform _barTransform;
        private float _currentViewRatio;
        private float _targetRatio;

        private float HealthRatio
        {
            get
            {
                if (_healthBehaviour != null)
                {
                    return _healthBehaviour.CurrentHealth / _healthBehaviour.MaxHealth;
                }
                else
                {
                    return 0f;
                }
            }
        }

        private void Awake()
        {
            Subscribe();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void Update()
        {
            if (_currentViewRatio != _targetRatio)
            {
                SetScale(Mathf.Lerp(_currentViewRatio, _targetRatio, Time.deltaTime * _lerp));
            }
        }

        public void SetHealthBehaviour(HealthBehaviour healthBehaviour)
        {
            Unsubscribe();
            _healthBehaviour = healthBehaviour;
            Subscribe();
        }

        private void Subscribe()
        {
            _healthBehaviour?.OnHealthChanged.AddListener(OnHealthChanged);
            _currentViewRatio = HealthRatio;
            OnHealthChanged(0f);
        }

        private void Unsubscribe()
        {
            _healthBehaviour?.OnHealthChanged.RemoveListener(OnHealthChanged);
        }

        private void OnHealthChanged(float healthChanged)
        {
            _targetRatio = HealthRatio;
        }

        private void SetScale(float ratio)
        {
            _currentViewRatio = ratio;
            _barTransform.localScale = new Vector3(_currentViewRatio, 1, 1);
        }
    }
}
