using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LigaGame.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private int _itensQuantity = 3;
        [SerializeField] private bool _createOnAwake = true;
        [SerializeField] private ScoreItem _itemPrefab;
        [SerializeField] private Transform _itemParent;
        private List<ScoreItem> _itens;
        private int _points = 0;

        private List<ScoreItem> Itens
        {
            get
            {
                if (_itens == null)
                {
                    _itens = new List<ScoreItem>();
                }
                return _itens;
            }
        }
        public int Points => _points;

        private void Awake()
        {
            if (_createOnAwake)
            {
                SetScoreItensQuantity(_itensQuantity);
            }
        }

        public void SetScoreItensQuantity(int quantity)
        {
            Clear();
            for (int itemId = 0; itemId < quantity; itemId++)
            {
                ScoreItem item = CreateItem();
                Itens.Add(item);
                item.SetState(ScoreItemState.Unmarked);
            }
        }

        public void SetPoints(int points)
        {
            _points = points;
            int pointsRemaining = points;
             
            foreach (ScoreItem item in Itens)
            {
                if (pointsRemaining > 0)
                {
                    item.SetState(ScoreItemState.Marked);
                    pointsRemaining--;
                }
                else
                {
                    item.SetState(ScoreItemState.Unmarked);
                }
            }
        }

        public void Inactive()
        {
            foreach (ScoreItem item in Itens)
            {
                item.SetState(ScoreItemState.Inactived);
            }
        }

        private void Clear()
        {
            foreach (ScoreItem item in Itens)
            {
                Destroy(item.gameObject);
            }
            Itens.Clear();
        }

        private ScoreItem CreateItem()
        {
            ScoreItem item = Instantiate(_itemPrefab, _itemParent);
            return item;
        }
    }
}
