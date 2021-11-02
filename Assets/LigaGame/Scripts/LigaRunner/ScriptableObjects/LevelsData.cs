using UnityEngine;

[CreateAssetMenu(fileName = "LevelsData", menuName = "ScriptableObjects/LevelsData", order = 1)]
public class LevelsData : ScriptableObject
{
    [SerializeField] private LevelData[] _levels;

    public LevelData[] Levels => _levels;
}

[System.Serializable]
public class LevelData
{
    public string Name;
    public string Scene;
    public Sprite Icon;
}