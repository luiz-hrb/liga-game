using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using LigaGame.Player;
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
        [SerializeField] private LevelManagerView _levelManagerView;
        [SerializeField] private PlayerSpawer _playerSpawer;
        [SerializeField] private CinemachineVirtualCamera _cinemachineCamera;
        [SerializeField] private Checkpoint _onCompleteLevelCheckpoint;
        [SerializeField] private PowerUp[] _pointsToCollect;
        [SerializeField] private LevelsData _levelsData;

        private PlayerController _player;
        private LevelModel _levelModel;
        private bool _playerWon;

        private void Awake()
        {
            _levelModel = _levelsData.GetLevelData(_sceneIndex);
            _player = _playerSpawer.SpawnPlayer();
            _cinemachineCamera.Follow = _player.transform;

            _levelManagerView.Initialize(this, _player, _levelModel.quantityPoints);
            
            AssignEvents();
        }

        private void AssignEvents()
        {
            _player.OnDeath.AddListener(() => OnPlayerDie());
            _onCompleteLevelCheckpoint.OnCheckpointReached.AddListener(() => OnFinishLevel());

            foreach (PowerUps.PowerUp point in _pointsToCollect)
            {
                point.OnTake.AddListener(() => OnPointCollected());
            }
        }

        private void OnPlayerDie()
        {
            if (_playerWon)
                return;

            _levelManagerView.OpenScreen((int)LevelManagerView.ScreenType.GameOver);

            Analytics.CustomEvent("PlayerDie", new Dictionary<string, object>
            {
                { "Level", _levelModel.name },
                { "Stars", _levelManagerView.ScorePoints.Points }
            });
        }

        private void OnFinishLevel()
        {
            _playerWon = true;
            _player.CanInteract = false;
            
            SubmitScore();

            Analytics.CustomEvent("PlayerWin", new Dictionary<string, object>
            {
                { "Level", _levelModel.name },
                { "Stars", _levelManagerView.ScorePoints.Points }
            });
        }

        private void SubmitScore()
        {
            ScoreModel scoreModel = new ScoreModel
                (_levelManagerView.ScorePoints.Points,
                _levelModel.quantityPoints,
                _levelManagerView.Timer.ElapsedTime,
                _levelModel);
            
            _levelManagerView.OpenScreen((int)LevelManagerView.ScreenType.LevelCompleted, scoreModel);
            ScoreManager.Submit(scoreModel, _levelModel);
        }

        private void OnPointCollected()
        {
           _levelManagerView.ScorePoints.SetPoints(_levelManagerView.ScorePoints.Points + 1);
            
            Analytics.CustomEvent("CollectedStar", new Dictionary<string, object>
            {
                { "Level", _levelModel.name },
            });
        }
    }
}
