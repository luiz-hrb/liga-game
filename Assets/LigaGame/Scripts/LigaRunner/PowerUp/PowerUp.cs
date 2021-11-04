using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace LigaGame.PowerUps
{
    [RequireComponent(typeof(Animator))]
    public sealed class PowerUp : MonoBehaviour
    {
        [SerializeField] private float _time = 10f;
        private PowerUpBehaviour[] _behaviours;
        private PowerUpTarget _target;
        private Animator _animator;
        private AudioSource _audioSource;
        private bool _executed;

        public UnityEvent OnTake;
        
        private void Awake()
        {
            _behaviours = GetComponents<PowerUpBehaviour>();
            _audioSource = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_executed)
            {
                return;
            }

            PowerUpTarget target = other.GetComponentInParent<PowerUpTarget>();

            if (target)
            {
                ExecuteAction(target);
                _executed = true;
            }
        }

        private void ExecuteAction(PowerUpTarget target)
        {
            this._target = target;
            StartCoroutine(ExecuteAction());
        }

        private IEnumerator ExecuteAction()
        {
            OnTake.Invoke();
            SetVisible(false);
            EnableBehaviours(true);
            yield return new WaitForSeconds(_time);

            EnableBehaviours(false);
            Destroy();
        }

        private void EnableBehaviours(bool enable)
        {
            foreach (PowerUpBehaviour behaviour in _behaviours)
            {
                if (enable)
                {
                    behaviour.Target = _target;
                    _target.EnablePowerUp(behaviour);
                }
                else
                {
                    _target.DisablePowerUp(behaviour);
                }
            }
        }

        public void SetVisible(bool isVisible)
        {
            _animator.SetBool("IsVisible", isVisible);
            
            if (!isVisible)
            {
                if (_audioSource)
                {
                    _audioSource.Play();
                }
            }
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
