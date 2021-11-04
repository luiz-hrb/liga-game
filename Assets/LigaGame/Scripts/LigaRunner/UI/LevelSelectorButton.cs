using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LigaGame.Model;
using LigaGame.UI.Score;
using LigaGame.ExtensionMethods;

namespace LigaGame.UI
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

        public void Initialize(LevelModel levelData, LevelProgressModel levelProgressData, Action onClickAction = null)
        {
            _nameText.text = levelData.name;
            _iconImage.sprite = levelData.icon;
            _scoreMarks.SetScoreItensQuantity(levelData.quantityPoints);
            MarkProgress(levelProgressData);

            if (onClickAction != null)
            {
                _button.onClick.AddListener(() => onClickAction());
            }
        }

        public void MarkProgress(LevelProgressModel levelProgressData)
        {
            if (levelProgressData.completed)
            {
                _scoreMarks.SetPoints(levelProgressData.points);
                _timeText.text = levelProgressData.gameplayTime.FromSecondsToTime();
            }
            else
            {
                _scoreMarks.Inactive();
            }
        }
    }
}
