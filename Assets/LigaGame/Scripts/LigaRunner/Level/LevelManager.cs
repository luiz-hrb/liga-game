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
        [SerializeField] private Checkpoint _onCompleteLevelCheckpoint;
        [SerializeField] private PowerUp[] _starsToCollect;
        [SerializeField] private LevelsData _levelsData;

        [SerializeField] private LevelManagerView _levelCanvasManager;

        private PlayerController _player;
        private LevelModel _levelModel;
        private bool _playerWon;

        private void Awake()
        {
            _player = _playerSpawer.SpawnPlayer();
            _levelCanvasManager.Healthbar.SetHealthBehaviour(_player.HealthBehaviour);
            _levelModel = _levelsData.GetLevelData(_sceneIndex);
            _cinemachineCamera.Follow = _player.transform;

            _levelCanvasManager.ScoreStars.SetScoreItensQuantity(_levelModel.QuantityStars);
            _levelCanvasManager.ScoreStars.SetPoints(0);
            _levelCanvasManager.LevelManager = this;
            
            _levelCanvasManager.Timer.StartCount();
            AssignEvents();
        }

        private void AssignEvents()
        {
            _player.OnDeath.AddListener(() => OnPlayerDie());
            _onCompleteLevelCheckpoint.OnCheckpointReached.AddListener(() => OnFinishLevel());

            foreach (PowerUps.PowerUp star in _starsToCollect)
            {
                star.OnTake.AddListener(() => OnStarCollected());
            }
        }

        private void OnPlayerDie()
        {
            if (_playerWon)
                return;

            _levelCanvasManager.OpenScreen((int)LevelManagerView.ScreenType.GameOver);
            _levelCanvasManager.Timer.PauseCount();

            Analytics.CustomEvent("PlayerDie", new Dictionary<string, object>
            {
                { "Level", _levelModel.Name },
                { "Stars", _levelCanvasManager.ScoreStars.Points }
            });
        }

        private void OnFinishLevel()
        {
            _levelCanvasManager.Timer.PauseCount();
            _playerWon = true;
            _player.CanInteract = false;
            
            SubmitScore();

            Analytics.CustomEvent("PlayerWin", new Dictionary<string, object>
            {
                { "Level", _levelModel.Name },
                { "Stars", _levelCanvasManager.ScoreStars.Points }
            });
        }

        private void SubmitScore()
        {
            (int score, int maxScore, float time, LevelModel levelModel) scoreData =
                (_levelCanvasManager.ScoreStars.Points,
                _levelModel.QuantityStars,
                _levelCanvasManager.Timer.ElapsedTime,
                _levelModel);
            
            _levelCanvasManager.OpenScreen((int)LevelManagerView.ScreenType.LevelCompleted, scoreData);
            ScoreManager.Submit(scoreData.score, scoreData.time, _levelModel);
        }

        private void OnStarCollected()
        {
           _levelCanvasManager.ScoreStars.SetPoints(_levelCanvasManager.ScoreStars.Points + 1);
            
            Analytics.CustomEvent("CollectedStar", new Dictionary<string, object>
            {
                { "Level", _levelModel.Name },
            });
        }
    }
}
