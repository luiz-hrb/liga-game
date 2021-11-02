using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LigaGame.Menu.LevelSelector
{
    public class LevelSelector : MonoBehaviour
    {
        [SerializeField] private LevelsData _levelsData;
        [SerializeField] private LevelSelectorButton _levelButtonPrefab;
        [SerializeField] private Transform _levelButtonParent;
        private List<LevelSelectorButton> _buttons;

        private void Start()
        {
            _buttons = new List<LevelSelectorButton>();
            CreateButtons();
        }

        private void CreateButtons()
        {
            ClearButtons();

            foreach (LevelData levelData in _levelsData.Levels)
            {
                LevelSelectorButton button = CreateButton(levelData);
                _buttons.Add(button);
            }
        }

        private LevelSelectorButton CreateButton(LevelData levelData)
        {
            var button = Instantiate(_levelButtonPrefab, _levelButtonParent);
            button.Init(levelData, 3);
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
