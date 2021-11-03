using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LigaGame.LoadScenes;
using LigaGame.Save;
using LigaGame.ScriptableObjects;

namespace LigaGame
{
    public static class ScoreManager
    {
        public static void Submit(int points, float time, LevelData levelData)
        {
            LevelProgressData levelProgressData = SaveSystem.PlayerData.GetLevelProgressData(levelData.Scene);

            if (!levelProgressData.Completed
                || levelProgressData.Points < points
                || levelProgressData.Points == points && levelProgressData.GameplayTime > time
            )
            {
                levelProgressData.SetScore(points, time);
                SaveSystem.SavePlayer();
            }
        }
    }
}
