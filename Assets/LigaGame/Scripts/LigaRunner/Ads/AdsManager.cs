using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Advertisements;

namespace LigaGame.Ads
{
    public class AdsManager : MonoBehaviour
    {
        [SerializeField] private Button _showAdButton;
        private string _adUnitId = "Interstitial_Android";

        private const string _gameId = "4434469"; 

        public UnityEvent OnAdsFinished;

        private void Start()
        {
            Advertisement.Initialize(_gameId);
            _showAdButton.onClick.AddListener(ShowAd);
        }

        public void ShowAd()
        {
            if (Advertisement.IsReady(_adUnitId))
            {
                _showAdButton.interactable = false;
                Advertisement.Show(_adUnitId);
            }
        }
    }
}
