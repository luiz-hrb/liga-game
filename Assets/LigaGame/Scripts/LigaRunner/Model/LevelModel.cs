using UnityEngine;
using LigaGame.LoadScene;

namespace LigaGame.Model
{
    [System.Serializable]
    public class LevelModel
    {
        public string Name;
        public ScenesIndex Scene;
        public Sprite Icon;
        public int QuantityStars = 3;
    }
}
