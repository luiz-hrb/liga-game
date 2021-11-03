using LigaGame.LoadScene;

namespace LigaGame.Model
{
    [System.Serializable]
    public class LevelProgressModel
    {
        public bool Completed;
        public int Points;
        public float GameplayTime;
        public ScenesIndex Scene;

        public LevelProgressModel()
        {

        }
        
        public LevelProgressModel(int points, float gameplayTime, ScenesIndex scene)
        {
            Completed = true;
            Points = points;
            GameplayTime = gameplayTime;
            Scene = scene;
        }
        
        public LevelProgressModel(ScenesIndex scene)
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
}
