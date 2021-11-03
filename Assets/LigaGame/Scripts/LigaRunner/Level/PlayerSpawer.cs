using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LigaGame.Player;

namespace LigaGame
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
