using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LigaGame.Sound
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _clips;
        [SerializeField] private AudioSource _audioSourcePrefab;
        private List<AudioSource> _audioSourcePool;
        public static SoundManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                _audioSourcePool = new List<AudioSource>();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlaySound(int soundIndex, Vector3 position)
        {
            AudioSource audioSource = GetAudioSource();
            audioSource.clip = _clips[soundIndex];
            audioSource.transform.position = position;
        }

        private AudioSource GetAudioSource()
        {
            AudioSource audioSource = GetAudioSourceFromPool();
            if (audioSource == null)
            {
                audioSource = InstantiteAudioSource();
            }
            return audioSource;
        }

        private AudioSource GetAudioSourceFromPool()
        {
            // Get a audio source that inst used anymore
            return null;
        }

        private AudioSource InstantiteAudioSource()
        {
            AudioSource audioSource = Instantiate(_audioSourcePrefab, transform);
            _audioSourcePool.Add(audioSource);
            return audioSource;
        }
    }
}
