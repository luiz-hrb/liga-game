using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Advertisements;
using UnityEngine.Analytics;

namespace LigaGame.Services
{
    [RequireComponent(typeof(Button))]
    public class AdsButton : MonoBehaviour
    {
        [SerializeField] private string _adUnitId = "Interstitial_Android";
        private Button _showAdButton;

        public UnityEvent OnAdsFinished;

        private void Awake()
        {
            _showAdButton = GetComponent<Button>();
            _showAdButton.onClick.AddListener(ShowAd);
        }

        public void ShowAd()
        {
            if (Advertisement.IsReady(_adUnitId))
            {
                Advertisement.Show(_adUnitId);
                OnAdsFinished.Invoke();

                Analytics.CustomEvent("Viwed Ads");
            }
        }
    }
}
