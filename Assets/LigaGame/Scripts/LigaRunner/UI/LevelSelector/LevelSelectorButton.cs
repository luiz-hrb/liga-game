using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LigaGame.ScriptableObjects;

namespace LigaGame.UI.Menu.LevelSelector
{
    public class LevelSelectorButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private Image _iconImage;
        [SerializeField] private Button _button;
        [SerializeField] private MarkablesHolder _scoreMarks;

        public Image _IconImage => _iconImage;
        public TextMeshProUGUI _TitleText => _nameText;

        public void Initialize(LevelData levelData, LevelProgressData levelProgressData, Action onClickAction = null)
        {
            _nameText.text = levelData.Name;
            _iconImage.sprite = levelData.Icon;
            _scoreMarks.SetQuantityMarks(levelData.QuantityStars);
            MarkProgress(levelProgressData);

            if (onClickAction != null)
            {
                _button.onClick.AddListener(() => onClickAction());
            }
        }

        public void MarkProgress(LevelProgressData levelProgressData)
        {
            if (levelProgressData.completed)
            {
                _scoreMarks.Mark(levelProgressData.starsCollected);
            }
            else
            {
                _scoreMarks.Inactive();
            }
        }
    }
}
