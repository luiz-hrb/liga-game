using System.Collections;
using UnityEngine;
using TMPro;

namespace LigaGame.UI
{
    public class Notification : MonoBehaviour
    {
        [SerializeField] private float _duration = 2f;
        [SerializeField] private TextMeshProUGUI _messageText;
        [SerializeField] private Animator _animator;
        private Coroutine _coroutineShow;

        public static Notification Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        public void Message(string message)
        {
            _messageText.text = message;

            if (_coroutineShow != null)
            {
                StopCoroutine(_coroutineShow);
            }
            _coroutineShow = StartCoroutine(Showing());
        }

        private IEnumerator Showing()
        {
            ShowAnimation(true);
            yield return new WaitForSeconds(_duration);

            ShowAnimation(false);
        }

        private void ShowAnimation(bool show)
        {
            Debug.Log($"show {show}");
            _animator.SetBool("IsShowing", show);

            if (show)
            {
                _animator.SetTrigger("ShowNow");
            }
        }
    }
}
