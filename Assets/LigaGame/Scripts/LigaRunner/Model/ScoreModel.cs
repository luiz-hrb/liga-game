using UnityEngine;
using LigaGame.LoadScene;

namespace LigaGame.Model
{
    [System.Serializable]
    public class ScoreModel
    {
        public int points;
        public int maxScore;
        public float time;
        public LevelModel levelModel;

        public ScoreModel()
        {
        }

        public ScoreModel(int score, int maxScore, float time, LevelModel levelModel)
        {
            this.points = score;
            this.maxScore = maxScore;
            this.time = time;
            this.levelModel = levelModel;
        }
    }
}
