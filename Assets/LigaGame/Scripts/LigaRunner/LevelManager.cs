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
        private PlayerController _player;

        private void Start()
        {
            _player = _playerSpawer.SpawnPlayer();
            _cinemachineCamera.Follow = _player.transform;
            _timer.StartCount();
        }
    }
}
