using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using LigaGame.Save;
using LigaGame.ScriptableObjects;
using LigaGame.LoadScene;
using LigaGame.Model;

namespace LigaGame.UI.Screens
{
    public class LevelSelectorScreen : ScreenBase
    {
        [SerializeField] private LevelsData _levelsData;
        [SerializeField] private LevelSelectorButton _levelButtonPrefab;
        [SerializeField] private Transform _levelButtonParent;
        [SerializeField] private Button _returnButton;
        private List<LevelSelectorButton> _buttons;

        private new void Awake()
        {
            base.Awake();
            _buttons = new List<LevelSelectorButton>();
            AssignEvents();

            SaveSystem.PlayerData.CheckLevelsProgressData(_levelsData.Levels);
            CreateLoadMenu();
        }

        private void AssignEvents()
        {
            _returnButton.onClick.AddListener(Return);
        }

        private void CreateLoadMenu()
        {
            ClearButtons();
            var levelsProgressData = SaveSystem.PlayerData.levelsProgressData;

            for (int levelId = 0; levelId < levelsProgressData.Length; levelId++)
            {
                LevelSelectorButton button = CreateButton(_levelsData.Levels[levelId], levelsProgressData[levelId]);
                _buttons.Add(button);
            }
        }

        private LevelSelectorButton CreateButton(LevelModel levelData, LevelProgressModel levelProgressData)
        {
            var button = Instantiate(_levelButtonPrefab, _levelButtonParent);

            button.Initialize(levelData, levelProgressData, onClickAction: () => {
                Analytics.CustomEvent("Level loaded", new Dictionary<string, object>
                {
                    { "Level", levelData.name }
                });
                
                SceneLoader.Instance.LoadLevelAsync(levelData.scene);
            });
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
