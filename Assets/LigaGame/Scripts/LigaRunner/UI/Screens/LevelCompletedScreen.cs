using UnityEngine;
using UnityEngine.UI;
using LigaGame.LoadScene;
using LigaGame.ExtensionMethods;
using LigaGame.UI.Score;
using LigaGame.ScriptableObjects;
using LigaGame.Model;
using TMPro;

namespace LigaGame.UI.Screens
{
    public class LevelCompletedScreen : ScreenBase
    {
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private ScoreView _scoreStars;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private LevelsData _levelsData;
        private LevelModel _levelModel;
        private ScenesIndex _nextScene = ScenesIndex.None;

        private new void Awake()
        {
            base.Awake();

            AssignEvents();
        }

        private void AssignEvents()
        {
            _nextLevelButton.onClick.AddListener(OnNextLevelButtonClick);
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
        }

        private void OnNextLevelButtonClick()
        {
            if (_nextScene != ScenesIndex.None)
            {
                SceneLoader.Instance.LoadLevelAsync(_nextScene);
            }
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
                ScoreModel scoreModel = (ScoreModel) args;
                
                _timeText.text = scoreModel.time.FromSecondsToTime();
                _scoreStars.SetScoreItensQuantity(scoreModel.maxScore);
                _scoreStars.SetPoints(scoreModel.points);
                _levelModel = scoreModel.levelModel;

                _nextScene = GetNextScene();
                bool hasNextLevel = _nextScene != ScenesIndex.None;
                _nextLevelButton.gameObject.SetActive(hasNextLevel);
            }
            else
            {
                _timeText.text = 0f.FromSecondsToTime();
                _scoreStars.SetScoreItensQuantity(0);
            }
        }

        private ScenesIndex GetNextScene()
        {
            LevelModel nextLevel = _levelsData.GetNextLevel(_levelModel);

            if (nextLevel != null)
            {
                return nextLevel.scene;
            }
            else
            {
                return ScenesIndex.None;
            }
        }
    }
}
