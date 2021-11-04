using LigaGame.LoadScene;

namespace LigaGame.Model
{
    [System.Serializable]
    public class LevelProgressModel
    {
        public bool completed;
        public int points;
        public float gameplayTime;
        public ScenesIndex scene;

        public LevelProgressModel()
        {

        }
        
        public LevelProgressModel(int points, float gameplayTime, ScenesIndex scene)
        {
            completed = true;
            this.points = points;
            this.gameplayTime = gameplayTime;
            this.scene = scene;
        }
        
        public LevelProgressModel(ScenesIndex scene)
        {
            this.scene = scene;
        }
        
        public void SetScore(ScoreModel scoreModel)
        {
            completed = true;
            points = scoreModel.points;
            gameplayTime = scoreModel.time;
        }
    }
}
