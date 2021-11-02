using System.Collections;
using System.Collections.Generic;
using LigaGame.UI.Save;


[System.Serializable]
public class PlayerData
{
    public LevelProgressData[] levelsProgressData;
        
    public void SetLevelsQuantity(int targetLevelsQuantity)
    {
        bool changedPlayerData = false;

        if (levelsProgressData == null)
        {
            levelsProgressData = new LevelProgressData[targetLevelsQuantity];
            changedPlayerData = true;
        }
        else if (levelsProgressData.Length != targetLevelsQuantity)
        {
            var newLevelsProgressData = new LevelProgressData[targetLevelsQuantity];
            
            for (int levelId = 0; levelId < levelsProgressData.Length && levelId < targetLevelsQuantity; levelId++)
            {
                newLevelsProgressData[levelId] = levelsProgressData[levelId];
            }
            levelsProgressData = newLevelsProgressData;
            changedPlayerData = true;
        }

        if (changedPlayerData)
        {
            SaveSystem.SavePlayer();
        }
    }

    public bool PopulateNullLevels()
    {
        bool dataChanged = false;

        for (int i = 0; i < levelsProgressData.Length; i++)
        {
            if (levelsProgressData[i] == null)
            {
                levelsProgressData[i] = new LevelProgressData();
                dataChanged = true;
            }
        }
        return dataChanged;
    }
}
