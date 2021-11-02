using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LigaGame.Save
{
    public static class SaveSystem
    {
        private const string PLAYER_KEY_NAME = "player";

        public static void SavePlayer(PlayerSaveData playerData)
        {
            string json = JsonUtility.ToJson(playerData);
            PlayerPrefs.SetString(PLAYER_KEY_NAME, json);
        }

        public static PlayerSaveData LoadPlayer()
        {
            string json = PlayerPrefs.GetString(PLAYER_KEY_NAME);
            
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            else
            {
                PlayerSaveData playerData = JsonUtility.FromJson<PlayerSaveData>(json);
                return playerData;
            }
        }
    }
}
