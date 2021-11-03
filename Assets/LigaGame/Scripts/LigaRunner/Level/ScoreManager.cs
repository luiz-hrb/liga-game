using LigaGame.Save;
using LigaGame.Model;

namespace LigaGame.Level
{
    public static class ScoreManager
    {
        public static void Submit(int points, float time, LevelModel levelData)
        {
            LevelProgressModel levelProgressData = SaveSystem.PlayerData.GetLevelProgressModel(levelData.Scene);

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
