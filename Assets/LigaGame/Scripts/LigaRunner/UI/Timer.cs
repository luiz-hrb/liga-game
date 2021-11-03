using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using LigaGame.ExtensionMethods;

namespace LigaGame.UI
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private bool _isCounting;
        private float _elapsedTime;

        public float ElapsedTime => _elapsedTime;

        private void Update()
        {
            if (_isCounting)
            {
                SetTime(_elapsedTime + Time.deltaTime);
            }
        }

        public void StartCount()
        {
            _isCounting = true;
        }

        public void PauseCount()
        {
            _isCounting = false;
        }

        public void StopCount()
        {
            _isCounting = false;
            SetTime(0f);
        }

        private void SetTime(float time)
        {
            _elapsedTime = time;
            TimeSpan timeSpan = TimeSpan.FromSeconds(_elapsedTime);
            _timeText.text = _elapsedTime.FromSecondsToTime();
        }
    }
}
