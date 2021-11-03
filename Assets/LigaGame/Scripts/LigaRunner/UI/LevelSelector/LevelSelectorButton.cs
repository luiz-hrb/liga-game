using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LigaGame.ScriptableObjects;
using LigaGame.ExtensionMethods;

namespace LigaGame.UI.Menu.LevelSelector
{
    public class LevelSelectorButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private Image _iconImage;
        [SerializeField] private Button _button;
        [SerializeField] private ScoreView _scoreMarks;

        public Image _IconImage => _iconImage;
        public TextMeshProUGUI _TitleText => _nameText;

        public void Initialize(LevelData levelData, LevelProgressData levelProgressData, Action onClickAction = null)
        {
            _nameText.text = levelData.Name;
            _timeText.text = levelProgressData.GameplayTime.FromSecondsToTime();
            _iconImage.sprite = levelData.Icon;
            _scoreMarks.SetScoreItensQuantity(levelData.QuantityStars);
            MarkProgress(levelProgressData);

            if (onClickAction != null)
            {
                _button.onClick.AddListener(() => onClickAction());
            }
        }

        public void MarkProgress(LevelProgressData levelProgressData)
        {
            if (levelProgressData.Completed)
            {
                _scoreMarks.SetPoints(levelProgressData.Points);
            }
            else
            {
                _scoreMarks.Inactive();
            }
        }
    }
}
