using UnityEngine;
using UnityEngine.UI;
using LigaGame.LoadScene;
using LigaGame.ExtensionMethods;
using LigaGame.UI.Score;
using TMPro;

namespace LigaGame.UI.Screens
{
    public class OnWinScreen : ScreenBase
    {
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private ScoreView _scoreStars;
        [SerializeField] private Button _mainMenuButton;

        private new void Awake()
        {
            base.Awake();
            AssignEvents();
        }

        private void AssignEvents()
        {
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
        }

        private void OnMainMenuButtonClick()
        {
            SceneLoader.Instance.LoadLevelAsync(ScenesIndex.MENU);
        }

        public override void Appear(bool appear, object args)
        {
            base.Appear(appear, args);

            if (args != null)
            {
                (int score, int maxScore, float time) screenArgs = ((int, int, float)) args;
                _timeText.text = screenArgs.time.FromSecondsToTime();
                _scoreStars.SetScoreItensQuantity(screenArgs.maxScore);
                _scoreStars.SetPoints(screenArgs.score);
            }
            else
            {
                _timeText.text = 0f.FromSecondsToTime();
                _scoreStars.SetScoreItensQuantity(0);
            }
        }
    }
}
