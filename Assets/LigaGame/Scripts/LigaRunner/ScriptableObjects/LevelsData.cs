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
                if (level.scene == scene)
                {
                    return level;
                }
            }
            return null;
        }

        public LevelModel GetNextLevel(LevelModel currentLevel)
        {
            int currentLevelIndex = GetLevelIndex(currentLevel);

            if (currentLevelIndex == -1)
            {
                return null;
            }
            
            if (currentLevelIndex == _levels.Length - 1)
            {
                return null;
            }
            return _levels[currentLevelIndex + 1];
        }

        private int GetLevelIndex(LevelModel level)
        {
            for (int levelId = 0; levelId < _levels.Length; levelId++)
            {
                if (_levels[levelId].scene == level.scene)
                {
                    return levelId;
                }
            }
            return -1;
        }
    }
}
