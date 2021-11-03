using LigaGame.Save;
using LigaGame.ScriptableObjects;
using LigaGame.LoadScene;

namespace LigaGame.Model
{
    [System.Serializable]
    public class PlayerModel
    {
        public LevelProgressModel[] LevelsProgressData;
            
        public void CheckLevelsData(LevelModel[] levelsData)
        {
            CheckLevelsProgressNull(levelsData);
            CheckLevelsQuantity(levelsData);
        }

        private void CheckLevelsProgressNull(LevelModel[] levelsData)
        {
            if (LevelsProgressData == null)
            {
                LevelsProgressData = new LevelProgressModel[levelsData.Length];

                for (int i = 0; i < LevelsProgressData.Length; i++)
                {
                    LevelsProgressData[i] = new LevelProgressModel(levelsData[i].Scene);
                }

                SaveSystem.SavePlayer();
            }
        }

        private void CheckLevelsQuantity(LevelModel[] levelsData)
        {
            if (LevelsProgressData.Length != levelsData.Length)
            {
                LevelProgressModel[] newLevelsProgressData = new LevelProgressModel[levelsData.Length];
                
                for (int levelId = 0; levelId < newLevelsProgressData.Length; levelId++)
                {
                    bool isNewLevel = levelId >= LevelsProgressData.Length;

                    if (!isNewLevel)
                    {
                        newLevelsProgressData[levelId] = LevelsProgressData[levelId];
                    }
                    else
                    {
                        newLevelsProgressData[levelId] = new LevelProgressModel(levelsData[levelId].Scene);
                    }
                }
                LevelsProgressData = newLevelsProgressData;

                SaveSystem.SavePlayer();
            }
        }

        public LevelProgressModel GetLevelProgressModel(ScenesIndex scene)
        {
            foreach (LevelProgressModel levelProgress in LevelsProgressData)
            {
                if (levelProgress.Scene == scene)
                {
                    return levelProgress;
                }
            }
            return null;
        }

        public void SetLevelProgressModel(ScenesIndex scene, LevelProgressModel newLevelProgress)
        {
            for (int i = 0; i < LevelsProgressData.Length; i++)
            {
                if (LevelsProgressData[i].Scene == scene)
                {
                    LevelsProgressData[i] = newLevelProgress;
                    break;
                }
            }
        }
    }
}
