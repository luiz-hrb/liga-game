using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LigaGame.Save;

namespace LigaGame.Menu.LevelSelector
{
    public class LevelSelector : MonoBehaviour
    {
        [SerializeField] private LevelsData _levelsData;
        [SerializeField] private LevelSelectorButton _levelButtonPrefab;
        [SerializeField] private Transform _levelButtonParent;
        private List<LevelSelectorButton> _buttons;

        private void Awake()
        {
            _buttons = new List<LevelSelectorButton>();

            SaveSystem.PlayerData.SetLevelsQuantity(_levelsData.Levels.Length);
        }

        private void Start()
        {
            CreateButtons();
        }

        private void CreateButtons()
        {
            ClearButtons();
            var levelsProgressData = SaveSystem.PlayerData.levelsProgressData;

            for (int i = 0; i < levelsProgressData.Length; i++)
            {
                LevelData levelData = _levelsData.Levels[i];
                LevelProgressData levelProgressData = levelsProgressData[i];

                LevelSelectorButton button = CreateButton(levelData, levelProgressData);
                _buttons.Add(button);
            }
        }

        private LevelSelectorButton CreateButton(LevelData levelData, LevelProgressData levelProgressData)
        {
            var button = Instantiate(_levelButtonPrefab, _levelButtonParent);
            button.Init(levelData, levelProgressData);
            return button;
        }

        private void ClearButtons()
        {
            foreach (LevelSelectorButton button in _buttons)
            {
                Destroy(button.gameObject);
            }
            _buttons.Clear();
        }
    }
}
