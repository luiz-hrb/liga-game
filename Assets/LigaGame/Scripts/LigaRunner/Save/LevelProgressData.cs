using System.Collections;
using System.Collections.Generic;
using LigaGame.LoadScenes;

[System.Serializable]
public class LevelProgressData
{
    public bool Completed;
    public int Points;
    public float GameplayTime;
    public ScenesIndex Scene;

    public LevelProgressData()
    {

    }
    
    public LevelProgressData(int points, float gameplayTime, ScenesIndex scene)
    {
        Completed = true;
        Points = points;
        GameplayTime = gameplayTime;
        Scene = scene;
    }
    
    public LevelProgressData(ScenesIndex scene)
    {
        Scene = scene;
    }
    
    public void SetScore(int points, float gameplayTime)
    {
        Completed = true;
        Points = points;
        GameplayTime = gameplayTime;
    }
}