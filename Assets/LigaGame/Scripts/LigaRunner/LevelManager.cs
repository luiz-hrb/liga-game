using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LigaGame.Player;
using LigaGame.UI;
using Cinemachine;
using LigaGame.PowerUps;

namespace LigaGame
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private PlayerSpawer _playerSpawer;
        [SerializeField] private CinemachineVirtualCamera _cinemachineCamera;
        [SerializeField] private Timer _timer;
        [SerializeField] private Checkpoint _lastCheckpoint;
        [SerializeField] private PowerUp[] _starsToCollect;
        private PlayerController _player;
        private int _starsCollected = 0;

        private void Awake()
        {
            _player = _playerSpawer.SpawnPlayer();
            _cinemachineCamera.Follow = _player.transform;
            
            _timer.StartCount();
            AssignEvents();
        }

        private void AssignEvents()
        {
            _player.OnDeath.AddListener(() => OnPlayerDie());
            _lastCheckpoint.OnCheckpointReached.AddListener(() => OnFinishLevel());

            foreach (PowerUp star in _starsToCollect)
            {
                star.OnTake.AddListener(() => OnStarCollected());
            }
        }

        private void OnPlayerDie()
        {
            Debug.Log("Player died");
            _timer.PauseCount();
        }

        private void OnFinishLevel()
        {
            Debug.Log("Finished level");
            _timer.PauseCount();
        }

        private void OnStarCollected()
        {
            _starsCollected++;
        }
    }
}
