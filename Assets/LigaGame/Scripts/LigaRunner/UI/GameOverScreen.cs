using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LigaGame.LoadScenes;

namespace LigaGame.UI
{
    public class GameOverScreen : ScreenBase
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _mainMenuButton;

        private new void Awake()
        {
            base.Awake();
            AssignEvents();
        }

        private void AssignEvents()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClick);
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
        }

        private void OnMainMenuButtonClick()
        {
            SceneLoader.Instance.LoadLevelAsync(ScenesIndex.MENU);
        }

        private void OnRestartButtonClick()
        {
            SceneLoader.Instance.ReloadThisScene();
        }

        public void OnDeath()
        {
            Time.timeScale = 0;
        }
    }
}
