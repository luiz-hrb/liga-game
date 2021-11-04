using LigaGame.Save;
using LigaGame.ScriptableObjects;
using LigaGame.LoadScene;

namespace LigaGame.Model
{
    [System.Serializable]
    public class PlayerModel
    {
        public LevelProgressModel[] levelsProgressData;
            
        public void CheckLevelsProgressData(LevelModel[] levelsData)
        {
            CheckLevelsProgressNull(levelsData);
            CheckLevelsQuantity(levelsData);
        }

        private void CheckLevelsProgressNull(LevelModel[] levelsData)
        {
            if (levelsProgressData == null)
            {
                levelsProgressData = new LevelProgressModel[levelsData.Length];

                for (int i = 0; i < levelsProgressData.Length; i++)
                {
                    levelsProgressData[i] = new LevelProgressModel(levelsData[i].scene);
                }

                SaveSystem.SavePlayer();
            }
        }

        private void CheckLevelsQuantity(LevelModel[] levelsData)
        {
            if (levelsProgressData.Length != levelsData.Length)
            {
                LevelProgressModel[] newLevelsProgressData = new LevelProgressModel[levelsData.Length];
                
                for (int levelId = 0; levelId < newLevelsProgressData.Length; levelId++)
                {
                    bool isNewLevel = levelId >= levelsProgressData.Length;

                    LevelProgressModel levelProgressData = isNewLevel ?
                        new LevelProgressModel(levelsData[levelId].scene) :
                        levelsProgressData[levelId];

                    newLevelsProgressData[levelId] = levelProgressData;
                }
                levelsProgressData = newLevelsProgressData;

                SaveSystem.SavePlayer();
            }
        }

        public LevelProgressModel GetLevelProgressModel(ScenesIndex scene)
        {
            foreach (LevelProgressModel levelProgress in levelsProgressData)
            {
                if (levelProgress.scene == scene)
                {
                    return levelProgress;
                }
            }
            return null;
        }

        public void SetLevelProgressModel(ScenesIndex scene, LevelProgressModel newLevelProgress)
        {
            for (int i = 0; i < levelsProgressData.Length; i++)
            {
                if (levelsProgressData[i].scene == scene)
                {
                    levelsProgressData[i] = newLevelProgress;
                    break;
                }
            }
        }
    }
}
