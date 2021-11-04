using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using LigaGame.Player;
using LigaGame.UI;
using LigaGame.UI.Score;
using LigaGame.UI.Screens;
using LigaGame.Model;
using LigaGame.ScriptableObjects;
using LigaGame.LoadScene;
using LigaGame.PowerUps;

namespace LigaGame.Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private ScenesIndex _sceneIndex;
        [SerializeField] private PlayerSpawer _playerSpawer;
        [SerializeField] private CinemachineVirtualCamera _cinemachineCamera;
        [SerializeField] private Timer _timer;
        [SerializeField] private Checkpoint _lastCheckpoint;
        [SerializeField] private PowerUp[] _starsToCollect;
        [SerializeField] private ScoreView _scoreStars;
        [SerializeField] private Healthbar _healthbar;
        [SerializeField] private LevelsData _levelsData;

        [SerializeField] private LevelCanvasManager _levelCanvasManager;

        private PlayerController _player;
        private LevelModel _levelModel;
        private bool _playerWon;

        private void Awake()
        {
            _player = _playerSpawer.SpawnPlayer();
            _healthbar.SetHealthBehaviour(_player.HealthBehaviour);
            _levelModel = _levelsData.GetLevelData(_sceneIndex);
            _cinemachineCamera.Follow = _player.transform;

            _scoreStars.SetScoreItensQuantity(_levelModel.QuantityStars);
            _scoreStars.SetPoints(0);
            _levelCanvasManager.LevelManager = this;
            
            _timer.StartCount();
            AssignEvents();
        }

        private void AssignEvents()
        {
            _player.OnDeath.AddListener(() => OnPlayerDie());
            _lastCheckpoint.OnCheckpointReached.AddListener(() => OnFinishLevel());

            foreach (PowerUps.PowerUp star in _starsToCollect)
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

            Analytics.CustomEvent("PlayerDie", new Dictionary<string, object>
            {
                { "Level", _levelModel.Name },
                { "Stars", _scoreStars.Points }
            });
        }

        private void OnFinishLevel()
        {
            _timer.PauseCount();
            _playerWon = true;
            _player.CanInteract = false;
            
            SubmitScore();

            Analytics.CustomEvent("PlayerWin", new Dictionary<string, object>
            {
                { "Level", _levelModel.Name },
                { "Stars", _scoreStars.Points }
            });
        }

        private void SubmitScore()
        {
            (int score, int maxScore, float time, LevelModel levelModel) scoreData = (_scoreStars.Points, _levelModel.QuantityStars, _timer.ElapsedTime, _levelModel);
            _levelCanvasManager.OpenScreen((int)LevelCanvasManager.ScreenType.Win, scoreData);
            ScoreManager.Submit(scoreData.score, scoreData.time, _levelModel);
        }

        private void OnStarCollected()
        {
            _scoreStars.SetPoints(_scoreStars.Points + 1);
            
            Analytics.CustomEvent("CollectedStar", new Dictionary<string, object>
            {
                { "Level", _levelModel.Name },
            });
        }
    }
}
