using System.Collections;
using System.Collections.Generic;
using LigaGame.Save;
using LigaGame.ScriptableObjects;
using LigaGame.LoadScenes;

[System.Serializable]
public class PlayerData
{
    public LevelProgressData[] LevelsProgressData;
        
    public void CheckLevelsData(LevelData[] levelsData)
    {
        CheckLevelsProgressNull(levelsData);
        CheckLevelsQuantity(levelsData);
    }

    private void CheckLevelsProgressNull(LevelData[] levelsData)
    {
        if (LevelsProgressData == null)
        {
            LevelsProgressData = new LevelProgressData[levelsData.Length];

            for (int i = 0; i < LevelsProgressData.Length; i++)
            {
                LevelsProgressData[i] = new LevelProgressData(levelsData[i].Scene);
            }

            SaveSystem.SavePlayer();
        }
    }

    private void CheckLevelsQuantity(LevelData[] levelsData)
    {
        if (LevelsProgressData.Length != levelsData.Length)
        {
            LevelProgressData[] newLevelsProgressData = new LevelProgressData[levelsData.Length];
            
            for (int levelId = 0; levelId < newLevelsProgressData.Length; levelId++)
            {
                bool isNewLevel = levelId >= LevelsProgressData.Length;

                if (!isNewLevel)
                {
                    newLevelsProgressData[levelId] = LevelsProgressData[levelId];
                }
                else
                {
                    newLevelsProgressData[levelId] = new LevelProgressData(levelsData[levelId].Scene);
                }
            }
            LevelsProgressData = newLevelsProgressData;

            SaveSystem.SavePlayer();
        }
    }

    // public bool PopulateNullLevels()
    // {
    //     bool dataChanged = false;

    //     for (int i = 0; i < LevelsProgressData.Length; i++)
    //     {
    //         if (LevelsProgressData[i] == null)
    //         {
    //             LevelsProgressData[i] = new LevelProgressData();
    //             dataChanged = true;
    //         }
    //     }
    //     return dataChanged;
    // }

    public LevelProgressData GetLevelProgressData(ScenesIndex scene)
    {
        foreach (LevelProgressData levelProgress in LevelsProgressData)
        {
            if (levelProgress.Scene == scene)
            {
                return levelProgress;
            }
        }
        return null;
    }

    public void SetLevelProgressData(ScenesIndex scene, LevelProgressData newLevelProgress)
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
