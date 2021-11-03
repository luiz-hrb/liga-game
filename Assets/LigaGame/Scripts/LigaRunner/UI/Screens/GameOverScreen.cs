using UnityEngine;
using UnityEngine.UI;
using LigaGame.LoadScene;

namespace LigaGame.UI.Screens
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
