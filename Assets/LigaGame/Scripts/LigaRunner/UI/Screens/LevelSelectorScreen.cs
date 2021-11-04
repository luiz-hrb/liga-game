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
            _returnButton.onClick.AddListener(Return);

            SaveSystem.PlayerData.CheckLevelsData(_levelsData.Levels);
        }

        private void Start()
        {
            CreateLoadMenu();
        }

        private void CreateLoadMenu()
        {
            ClearButtons();
            var levelsProgressData = SaveSystem.PlayerData.levelsProgressData;

            for (int i = 0; i < levelsProgressData.Length; i++)
            {
                LevelModel levelData = _levelsData.Levels[i];
                LevelProgressModel levelProgressData = levelsProgressData[i];

                LevelSelectorButton button = CreateButton(levelData, levelProgressData);
                _buttons.Add(button);
            }
        }

        private LevelSelectorButton CreateButton(LevelModel levelData, LevelProgressModel levelProgressData)
        {
            var button = Instantiate(_levelButtonPrefab, _levelButtonParent);
            button.Initialize(levelData, levelProgressData, () => {
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
