using UnityEngine;
using UnityEngine.Advertisements;

namespace LigaGame.Services
{
    public class AdsInitializer : MonoBehaviour
    {
        [SerializeField] private string _gameId = "4434469"; 

        private void Awake()
        {
            Advertisement.Initialize(_gameId);
        }
    }
}
