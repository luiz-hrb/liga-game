using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace LigaGame.UI.LoadScenes
{
    public sealed class LoadingDotsAnimation : MonoBehaviour
    {
        [SerializeField] private string _prefix;
        [SerializeField] private float _timeNewDot = 0.5f;
        [SerializeField] private int _dotsLimit = 3;
        [SerializeField] private TextMeshProUGUI _textLoading;
        private int _dotsCount;

        public void Initialize(string prefix, float timeNewDot, int dotsLimit, TextMeshProUGUI textLoading)
        {
            _prefix = prefix;
            _timeNewDot = timeNewDot;
            _dotsLimit = dotsLimit;
            _textLoading = textLoading;

            Awake();
        }

        private void Awake()
        {
            if (_textLoading == null)
            {
                _textLoading = GetComponent<TextMeshProUGUI>();
            }
            UpdateText();
            StartCoroutine(WaitingIncreaseDot());
        }

        IEnumerator WaitingIncreaseDot()
        {
            yield return new WaitForSeconds(_timeNewDot);

            if (gameObject.activeInHierarchy)
            {
                SetDotQuantity(_dotsCount + 1);
                StartCoroutine(WaitingIncreaseDot());
            }
        }

        private void SetDotQuantity(int dotsCount)
        {
            _dotsCount = dotsCount % (_dotsLimit + 1);
            UpdateText();
        }

        private void UpdateText()
        {
            _textLoading.text = $"{_prefix}{WriteDots(_dotsCount)}";
        }

        private string WriteDots(int quantity)
        {
            string text = "";

            for (int i = 0; i < quantity; i++)
            {
                text += ".";
            }
            return text;
        }
    }
}
