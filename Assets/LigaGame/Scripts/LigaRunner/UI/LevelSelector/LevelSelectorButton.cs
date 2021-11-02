using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LigaGame.UI.UI;

namespace LigaGame.UI.Menu.LevelSelector
{
    public class LevelSelectorButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private Image _iconImage;
        [SerializeField] private Button _button;
        [SerializeField] private Markable[] _scoreMarcs;

        public Image _IconImage => _iconImage;
        public TextMeshProUGUI _TitleText => _nameText;

        public void Initialize(LevelData levelData, LevelProgressData levelProgressData, Action onClickAction = null)
        {
            _nameText.text = levelData.Name;
            _iconImage.sprite = levelData.Icon;
            MarkProgress(levelProgressData);

            if (onClickAction != null)
            {
                _button.onClick.AddListener(() => onClickAction());
            }
        }

        public void MarkProgress(LevelProgressData levelProgressData)
        {
            for (int scoreMarkId = 0; scoreMarkId < _scoreMarcs.Length; scoreMarkId++)
            {
                Markable mark = _scoreMarcs[scoreMarkId];
                
                if (levelProgressData.completed)
                {
                    bool willMarkThisStar = scoreMarkId < levelProgressData.starsCollected;
                    mark.SetState(willMarkThisStar ? MarkableState.Marked : MarkableState.Unmarked);
                }
                else
                {
                    mark.SetState(MarkableState.Inactived);
                }
            }
        }
    }
}
