using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LigaGame.UI.UI.Menu
{
    public class MenuCanvasManager : Screen
    {
        public enum ScreenType
        {
            MainMenu = 0,
            LevelSelector = 1,
        }

        private void Start()
        {
            OpenScreen((int)ScreenType.MainMenu);
        }

        protected override void NotificationReceived(Screen child, string notification = null)
        {
            int childId = GetChildId(child);
            switch (childId)
            {
                case (int)ScreenType.MainMenu:
                {
                    switch (notification)
                    {
                        case "Play":
                        {
                            OpenScreen((int)ScreenType.LevelSelector);
                            break;
                        }

                        case "Quit":
                        {
                            Application.Quit();
                            break;
                        }
                    }
                    break;
                }

                case (int)ScreenType.LevelSelector:
                {
                    switch (notification)
                    {
                        case _returnNotification:
                        {
                            OpenScreen((int)ScreenType.MainMenu);
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }
}
