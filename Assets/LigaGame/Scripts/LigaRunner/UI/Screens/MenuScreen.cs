using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

namespace LigaGame.UI.Screens
{
    public sealed class MenuScreen : ScreenBase
    {
        [SerializeField] Button _buttonPlay;
        [SerializeField] Button _buttonQuit;

        private const string _playNotification = "Play";
        private const string _quitNotification = "Quit";

        private new void Awake()
        {
            base.Awake();
            
            _buttonPlay.onClick.AddListener(Play);
            _buttonQuit.onClick.AddListener(Quit);
        }

        public void Play()
        {
            NotifyParent(_playNotification);

            Analytics.CustomEvent("Play");
        }

        public void Quit()
        {
            NotifyParent(_quitNotification);
            
            Analytics.CustomEvent("Quit");
        }
    }
}
