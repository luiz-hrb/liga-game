using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LigaGame.LoadScenes;
using LigaGame.ExtensionMethods;
using TMPro;

namespace LigaGame.UI
{
    public class OnWinScreen : ScreenBase
    {
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private MarkablesHolder _scoreStars;
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
                _scoreStars.SetQuantityMarks(screenArgs.maxScore);
                _scoreStars.Mark(screenArgs.score);
            }
            else
            {
                _timeText.text = 0f.FromSecondsToTime();
                _scoreStars.SetQuantityMarks(0);
            }
        }
    }
}
