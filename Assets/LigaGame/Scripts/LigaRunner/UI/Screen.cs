using System.Collections.Generic;
using UnityEngine;

namespace LigaGame.UI
{
    public class Screen : MonoBehaviour
    {
        [SerializeField] protected Screen[] _subScreens;
        private Screen _parent;
        protected int _currentScreenId;
        protected bool _appearing;

        public Screen Parent => _parent;
        public bool Appearing => _appearing;

        public const string _returnNotification = "return";

        protected void Awake()
        {
            InitializeChilds();
        }

        protected void InitializeChilds()
        {
            foreach (Screen screen in _subScreens)
            {
                screen._parent = this;
            }
        }

        public void OpenScreen(int screenId)
        {
            OpenScreen(screenId, null);
        }

        public void OpenScreen(int screenToOpenId, object args)
        {
            _currentScreenId = screenToOpenId;
            Screen screenToOpen = null;

            if (_currentScreenId >= 0)
            {
                screenToOpen = _subScreens[_currentScreenId];
            }

            OpenScreen(screenToOpen, args);
        }

        private void OpenScreen(Screen screenToOpen, object args)
        {
            List<Screen> screensAnalyzed = new List<Screen>();

            foreach (Screen screen in _subScreens)
            {
                if (!screensAnalyzed.Contains(screen))
                {
                    screensAnalyzed.Add(screen);
                    screen.Appear(screen == screenToOpen, args);
                }
            }

            if (_appearing && screenToOpen == null)
            {
                OnAppear();
            }
        }

        protected int GetChildId(Screen child)
        {
            for (int i = 0; i < _subScreens.Length; i++)
            {
                if (_subScreens[i] == child)
                {
                    return i;
                }
            }

            return -1;
        }

        public virtual void Appear(bool appear)
        {
            Appear(appear, null);
        }

        public virtual void Appear(bool appear, object args)
        {
            _appearing = appear;
            gameObject.SetActive(_appearing);

            if (_appearing)
            {
                OnAppear(args);
            }
            if (!_appearing)
            {
                OnDisappear(args);
            }
        }

        public virtual void OnAppear(object args = null)
        {

        }

        public virtual void OnDisappear(object args = null)
        {
            OpenScreen(-1);
        }

        public virtual void NotifyParent(string notification = null)
        {
            if (_parent != null)
            {
                _parent.NotificationReceived(this, notification);
            }
        }

        protected virtual void NotificationReceived(Screen child, string notification = null)
        {

        }

        public virtual void Return()
        {
            NotifyParent(_returnNotification);
        }
    }    
}
