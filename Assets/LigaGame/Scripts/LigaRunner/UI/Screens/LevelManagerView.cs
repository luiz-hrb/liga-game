using UnityEngine;
using LigaGame.Level;
using LigaGame.UI;
using LigaGame.UI.Score;

namespace LigaGame.UI.Screens
{
    public class LevelManagerView : ScreenBase
    {
        public enum ScreenType
        {
            LevelCompleted = 0,
            GameOver = 1,
        }

        [SerializeField] private Timer _timer;
        [SerializeField] private ScoreView _scoreStars;
        [SerializeField] private Healthbar _healthbar;
        private LevelManager _levelManager;

        public LevelManager LevelManager
        {
            get => _levelManager;
            set => _levelManager = value;
        }
        public Timer Timer => _timer;
        public ScoreView ScoreStars => _scoreStars;
        public Healthbar Healthbar => _healthbar;

        public void Initialize()
        {
            _timer.StartCount();

        }
    }
}
