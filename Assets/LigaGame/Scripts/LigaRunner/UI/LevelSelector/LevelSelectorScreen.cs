using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using LigaGame.Save;
using LigaGame.ScriptableObjects;
using LigaGame.LoadScenes;

namespace LigaGame.UI.Menu.LevelSelector
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
            CreateButtons();
        }

        private void CreateButtons()
        {
            ClearButtons();
            var levelsProgressData = SaveSystem.PlayerData.LevelsProgressData;

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
            button.Initialize(levelData, levelProgressData, () => {
                Analytics.CustomEvent("Level loaded", new Dictionary<string, object>
                {
                    { "Level", levelData.Name }
                });
                SceneLoader.Instance.LoadLevelAsync(levelData.Scene);
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
