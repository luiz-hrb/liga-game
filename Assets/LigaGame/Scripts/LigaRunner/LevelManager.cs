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
        [SerializeField] private ScoreView _scoreStars;
        [SerializeField] private Healthbar _healthbar;
        [SerializeField] private LevelsData _levelsData;
        [SerializeField] private ScenesIndex _sceneIndex;

        [SerializeField] private LevelCanvasManager _levelCanvasManager;

        private PlayerController _player;
        private LevelData _levelData;
        private bool _playerWon;

        private void Awake()
        {
            _player = _playerSpawer.SpawnPlayer();
            _healthbar.SetHealthBehaviour(_player.HealthBehaviour);
            _levelData = _levelsData.GetLevelData(_sceneIndex);
            _cinemachineCamera.Follow = _player.transform;

            _scoreStars.SetScoreItensQuantity(_levelData.QuantityStars);
            _scoreStars.SetPoints(0);
            _levelCanvasManager.LevelManager = this;
            
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
            if (_playerWon)
                return;

            _levelCanvasManager.OpenScreen((int)LevelCanvasManager.ScreenType.GameOver);
            _timer.PauseCount();
        }

        private void OnFinishLevel()
        {
            (int score, int maxScore, float time) screenArgs = (_scoreStars.Points, _levelData.QuantityStars, _timer.ElapsedTime);
            _levelCanvasManager.OpenScreen((int)LevelCanvasManager.ScreenType.Win, screenArgs);

            _timer.PauseCount();
            _playerWon = true;
            _player.CanInteract = false;
        }

        private void OnStarCollected()
        {
            _scoreStars.SetPoints(_scoreStars.Points + 1);
        }
    }
}
