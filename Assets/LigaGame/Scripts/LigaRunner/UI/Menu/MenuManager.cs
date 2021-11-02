using UnityEngine;
using UnityEngine.UI;
using LigaGame.LoadScenes;

namespace LigaGame.Menu
{
    public sealed class MenuManager : MonoBehaviour
    {
        [SerializeField] Button _buttonPlay;
        [SerializeField] Button _buttonExit;

        public void Initialize(Button buttonPlay, Button buttonExit)
        {
            _buttonPlay = buttonPlay;
            _buttonExit = buttonExit;
            
            Awake();
        }

        private void Awake()
        {
            _buttonPlay.onClick.AddListener(Play);
            _buttonExit.onClick.AddListener(Exit);
        }

        public void Play()
        {
            LevelLoader.Instance.LoadLevelAsync(ScenesIndex.GAMEPLAY);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}
