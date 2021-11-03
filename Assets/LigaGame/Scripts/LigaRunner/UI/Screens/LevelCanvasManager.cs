using UnityEngine;
using LigaGame.Level;

namespace LigaGame.UI.Screens
{
    public class LevelCanvasManager : ScreenBase
    {
        public enum ScreenType
        {
            Win = 0,
            GameOver = 1,
        }

        private LevelManager _levelManager;

        public LevelManager LevelManager
        {
            get => _levelManager;
            set => _levelManager = value;
        }
    }
}
