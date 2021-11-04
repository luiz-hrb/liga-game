using UnityEngine;
using LigaGame.Level;
using LigaGame.Player;
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
        [SerializeField] private ScoreView _scorePoints;
        [SerializeField] private Healthbar _healthbar;
        private LevelManager _levelManager;

        public LevelManager LevelManager
        {
            get => _levelManager;
            set => _levelManager = value;
        }
        public Timer Timer => _timer;
        public ScoreView ScorePoints => _scorePoints;
        public Healthbar Healthbar => _healthbar;

        public void Initialize(LevelManager levelManager, PlayerController player, int quantityPoints)
        {
            _levelManager = levelManager;
            Timer.StartCount();
            Healthbar.SetHealthBehaviour(player.HealthBehaviour);
            Debug.Log($"points {quantityPoints}");
            ScorePoints.SetScoreItensQuantity(quantityPoints);
            ScorePoints.SetPoints(0);
        }

        public override void OpenScreen(int screenToOpenId, object args)
        {
            base.OpenScreen(screenToOpenId, args);

            Timer.PauseCount();
        }
    }
}
