using LigaGame.Save;
using LigaGame.Model;

namespace LigaGame.Level
{
    public static class ScoreManager
    {
        public static void Submit(ScoreModel scoreModel, LevelModel levelData)
        {
            LevelProgressModel levelProgressData = SaveSystem.PlayerData.GetLevelProgressModel(levelData.scene);

            if (IsBestScore(scoreModel, levelProgressData))
            {
                levelProgressData.SetScore(scoreModel);
                SaveSystem.SavePlayer();
            }
        }

        private static bool IsBestScore(ScoreModel scoreModel, LevelProgressModel levelProgressData)
        {
            return !levelProgressData.completed
                || levelProgressData.points < scoreModel.points
                || levelProgressData.points == scoreModel.points && levelProgressData.gameplayTime > scoreModel.time;
        }
    }
}
