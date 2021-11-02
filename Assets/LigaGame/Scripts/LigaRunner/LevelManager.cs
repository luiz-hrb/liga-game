using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LigaGame.Player;
using LigaGame.UI;
using Cinemachine;

namespace LigaGame
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private PlayerSpawer _playerSpawer;
        [SerializeField] private CinemachineVirtualCamera _cinemachineCamera;
        [SerializeField] private Timer _timer;
        [SerializeField] private Checkpoint _lastCheckpoint;
        private PlayerController _player;

        private void Awake()
        {
            _player = _playerSpawer.SpawnPlayer();
            _player.OnDeath.AddListener(() => OnPlayerDie());
            _cinemachineCamera.Follow = _player.transform;
            
            _timer.StartCount();
            _lastCheckpoint.OnCheckpointReached.AddListener(() => OnFinishLevel());
        }

        private void OnPlayerDie()
        {
            Debug.Log("Player died");
        }

        private void OnFinishLevel()
        {
            Debug.Log("Finished level");
        }
    }
}
