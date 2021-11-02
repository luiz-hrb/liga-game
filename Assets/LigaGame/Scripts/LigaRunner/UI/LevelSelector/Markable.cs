using UnityEngine;
using UnityEngine.UI;

namespace LigaGame.UI
{
    public class Markable : MonoBehaviour
    {
        [SerializeField] float _alphaMarked = 1f;
        [SerializeField] float _alphaUnmarked = 0.5f;
        [SerializeField] Image _image;

        public void SetState(MarkableState state)
        {
            switch (state)
            {
                case MarkableState.Marked:
                {
                    Active(true);
                    SetAlphaColor(_alphaMarked);
                    break;
                }

                case MarkableState.Unmarked:
                {
                    Active(true);
                    SetAlphaColor(_alphaUnmarked);
                    break;
                }

                case MarkableState.Inactived:
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
