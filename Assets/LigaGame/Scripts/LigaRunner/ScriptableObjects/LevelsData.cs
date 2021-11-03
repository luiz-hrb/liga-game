using UnityEngine;
using LigaGame.LoadScenes;

namespace LigaGame.ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelsData", menuName = "ScriptableObjects/LevelsData", order = 1)]
    public class LevelsData : ScriptableObject
    {
        [SerializeField] private LevelData[] _levels;

        public LevelData[] Levels => _levels;

        public LevelData GetLevelData(ScenesIndex scene)
        {
            foreach (LevelData level in _levels)
            {
                if (level.Scene == scene)
                {
                    return level;
                }
            }
            return null;
        }
    }

    [System.Serializable]
    public class LevelData
    {
        public string Name;
        public ScenesIndex Scene;
        public Sprite Icon;
        public int QuantityStars = 3;
    }
}
