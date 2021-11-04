using LigaGame.Save;
using LigaGame.Model;

namespace LigaGame.Level
{
    public static class ScoreManager
    {
        public static void Submit(int points, float time, LevelModel levelData)
        {
            LevelProgressModel levelProgressData = SaveSystem.PlayerData.GetLevelProgressModel(levelData.scene);

            if (!levelProgressData.completed
                || levelProgressData.points < points
                || levelProgressData.points == points && levelProgressData.gameplayTime > time
            )
            {
                levelProgressData.SetScore(points, time);
                SaveSystem.SavePlayer();
            }
        }
    }
}
