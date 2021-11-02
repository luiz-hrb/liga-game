using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LigaGame.UI;

namespace LigaGame.Menu.LevelSelector
{
    public class LevelSelectorButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private Image _iconImage;
        [SerializeField] private Button _button;
        [SerializeField] private Markable[] _scoreMarcs;

        public Image _IconImage => _iconImage;
        public TextMeshProUGUI _TitleText => _nameText;

        public void Init(LevelData levelData, int starsToMark, Action onClickAction = null)
        {
            _nameText.text = levelData.Name;
            _iconImage.sprite = levelData.Icon;
            MarkStars(starsToMark);

            if (onClickAction != null)
            {
                _button.onClick.AddListener(() => onClickAction());
            }
        }

        public void MarkStars(int starsToMarkQuantity)
        {
            bool willActiveStars = starsToMarkQuantity >= 0;

            for (int starId = 0; starId < _scoreMarcs.Length; starId++)
            {
                Markable mark = _scoreMarcs[starId];
                if (willActiveStars)
                {
                    bool willMarkStar = starId < starsToMarkQuantity;
                    mark.SetState(willMarkStar ? MarkableState.Marked : MarkableState.Unmarked);
                }
                else
                {
                    mark.SetState(MarkableState.Inactived);
                }
            }
        }
    }
}
