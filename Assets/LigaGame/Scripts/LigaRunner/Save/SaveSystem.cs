using LigaGame.Model;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LigaGame.Save
{
    public static class SaveSystem
    {
        private static PlayerModel _playerData;
        private const string PLAYER_KEY_NAME = "player";

        public static PlayerModel PlayerData
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

        public static void SavePlayer(PlayerModel playerData)
        {
            _playerData = playerData;
            SavePlayer();
        }

        public static void SavePlayer()
        {
            string playerDataJson = JsonUtility.ToJson(PlayerData);
            PlayerPrefs.SetString(PLAYER_KEY_NAME, playerDataJson);
        }

        public static PlayerModel LoadPlayer()
        {
            string playerDataJson = PlayerPrefs.GetString(PLAYER_KEY_NAME);
            PlayerModel playerData = null;
            
            if (!string.IsNullOrEmpty(playerDataJson))
            {
                playerData = JsonUtility.FromJson<PlayerModel>(playerDataJson);
            }
            else
            {
                playerData = new PlayerModel();
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
