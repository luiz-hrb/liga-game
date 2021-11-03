using UnityEngine;
using UnityEngine.UI;

namespace LigaGame.UI
{
    public class ScoreItem : MonoBehaviour
    {
        [SerializeField] float _alphaMarked = 1f;
        [SerializeField] float _alphaUnmarked = 0.5f;
        [SerializeField] Image _image;

        public void SetState(ScoreItemState state)
        {
            switch (state)
            {
                case ScoreItemState.Marked:
                {
                    Active(true);
                    SetAlphaColor(_alphaMarked);
                    break;
                }

                case ScoreItemState.Unmarked:
                {
                    Active(true);
                    SetAlphaColor(_alphaUnmarked);
                    break;
                }

                case ScoreItemState.Inactived:
                {
                    Active(false);
                    break;
                }
            }
        }

        private void Active(bool active)
        {
            gameObject.SetActive(active);
        }

        private void SetAlphaColor(float alpha)
        {
            Color color = _image.color;
            color.a = alpha;
            _image.color = color;
        }
    }
}
