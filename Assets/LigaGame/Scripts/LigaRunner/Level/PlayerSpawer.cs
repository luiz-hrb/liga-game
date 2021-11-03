using UnityEngine;
using LigaGame.Player;

namespace LigaGame.Level
{
    public class PlayerSpawer : MonoBehaviour
    {
        [SerializeField] PlayerController _playerPrefab;

        public PlayerController SpawnPlayer()
        {
            var player = Instantiate(_playerPrefab);
            player.transform.position = transform.position;
            return player;
        }
    }
}
