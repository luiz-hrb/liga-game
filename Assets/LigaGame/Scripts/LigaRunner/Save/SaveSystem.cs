using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LigaGame.Save
{
    public static class SaveSystem
    {
        private static PlayerData _playerData;
        private const string PLAYER_KEY_NAME = "player";

        public static PlayerData PlayerData
        {
            get
            {
                if (_playerData == null)
                {
                    _playerData = LoadPlayer();
                }
                return _playerData;
            }
            set
            {
                _playerData = value;
                SavePlayer();
            }
        }

        public static void SavePlayer(PlayerData playerData)
        {
            _playerData = playerData;
            SavePlayer();
        }

        public static void SavePlayer()
        {
            string playerDataJson = JsonUtility.ToJson(PlayerData);
            PlayerPrefs.SetString(PLAYER_KEY_NAME, playerDataJson);
        }

        public static PlayerData LoadPlayer()
        {
            string playerDataJson = PlayerPrefs.GetString(PLAYER_KEY_NAME);
            PlayerData playerData = null;
            
            if (!string.IsNullOrEmpty(playerDataJson))
            {
                playerData = JsonUtility.FromJson<PlayerData>(playerDataJson);
            }
            else
            {
                playerData = new PlayerData();
            }
            return playerData;
        }

#if UNITY_EDITOR
        [MenuItem("SaveSystem/DeletePlayer")]
        public static void DeletePlayer()
        {
            PlayerPrefs.DeleteKey(PLAYER_KEY_NAME);
        }

        [MenuItem("SaveSystem/ClearAllData")]
        public static void ClearAllData()
        {
            PlayerPrefs.DeleteAll();
        }
#endif
    }
}
