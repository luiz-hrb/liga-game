using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LigaGame.PowerUp
{
    [RequireComponent(typeof(Animator))]
    public sealed class PowerUp : MonoBehaviour
    {
        [SerializeField] private float _time = 10f;
        [SerializeField] private GameObject _view;
        [SerializeField] private AudioClip _clip_appear;
        [SerializeField] private AudioClip _clip_disappear;
        public UnityEvent OnTake;
        private PowerUpTarget _target;
        private Animator _animator;
        private AudioSource _audioSource;
        private bool _executed;

        private void Awake()
        {
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

            PowerUpBehaviour[] behaviours = GetComponents<PowerUpBehaviour>();
            SetVisible(false);

            foreach (PowerUpBehaviour behaviour in behaviours)
            {
                behaviour.Target = _target;
                _target.EnablePowerUp(behaviour);
            }
            yield return new WaitForSeconds(_time);

            foreach (PowerUpBehaviour behaviour in behaviours)
            {
                _target.DisablePowerUp(behaviour);
            }
            
            Destroy();
        }

        private void PlayAudioClip(AudioClip clip)
        {
            if (_audioSource)
            {
                _audioSource.clip = clip;
                _audioSource.Play();
            }
        }

        public void SetVisible(bool isVisible)
        {
            _animator.SetBool("IsVisible", isVisible);
            
            if (isVisible)
            {
                PlayAudioClip(_clip_appear);
            }
            else
            {
                PlayAudioClip(_clip_disappear);
            }
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
