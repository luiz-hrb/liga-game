using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Advertisements;
using UnityEngine.Analytics;

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
                Analytics.CustomEvent("Viwed Ads");
                Advertisement.Show(_adUnitId);
            }
        }
    }
}
