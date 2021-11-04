using UnityEngine;
using LigaGame.LoadScene;

namespace LigaGame.Model
{
    [System.Serializable]
    public class LevelModel
    {
        public string name;
        public ScenesIndex scene;
        public Sprite icon;
        public int quantityPoints = 3;
    }
}
