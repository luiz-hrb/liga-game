using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LigaGame.Player;
using LigaGame.UI;
using Cinemachine;
using LigaGame.PowerUps;
using LigaGame.ScriptableObjects;
using LigaGame.LoadScenes;

namespace LigaGame
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private PlayerSpawer _playerSpawer;
        [SerializeField] private CinemachineVirtualCamera _cinemachineCamera;
        [SerializeField] private Timer _timer;
        [SerializeField] private Checkpoint _lastCheckpoint;
        [SerializeField] private PowerUp[] _starsToCollect;
        [SerializeField] private MarkablesHolder _scoreStars;
        [SerializeField] private Healthbar _healthbar;
        [SerializeField] private LevelsData _levelsData;
        [SerializeField] private ScenesIndex _sceneIndex;

        [SerializeField] ScreenBase _dieScreen;
        [SerializeField] ScreenBase _winScreen;

        private PlayerController _player;
        private LevelData _levelData;

        private void Awake()
        {
            _player = _playerSpawer.SpawnPlayer();
            _healthbar.SetHealthBehaviour(_player.HealthBehaviour);
            _levelData = _levelsData.GetLevelData(_sceneIndex);
            _cinemachineCamera.Follow = _player.transform;

            _scoreStars.SetQuantityMarks(_levelData.QuantityStars);
            _scoreStars.Mark(0);
            
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
            _dieScreen.Appear(true);
            _timer.PauseCount();
        }

        private void OnFinishLevel()
        {
            _winScreen.Appear(true);
            _timer.PauseCount();
        }

        private void OnStarCollected()
        {
            _scoreStars.Mark(_scoreStars.QuantityMaks + 1);
        }
    }
}
