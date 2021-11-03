using UnityEngine;
using LigaGame.LoadScene;
using LigaGame.Model;

namespace LigaGame.ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelsData", menuName = "ScriptableObjects/LevelsData", order = 1)]
    public class LevelsData : ScriptableObject
    {
        [SerializeField] private LevelModel[] _levels;

        public LevelModel[] Levels => _levels;

        public LevelModel GetLevelData(ScenesIndex scene)
        {
            foreach (LevelModel level in _levels)
            {
                if (level.Scene == scene)
                {
                    return level;
                }
            }
            return null;
        }
    }
}
