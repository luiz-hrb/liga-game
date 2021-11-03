using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LigaGame.UI
{
    public class MarkablesHolder : MonoBehaviour
    {
        [SerializeField] private int _markablesQuantity = 3;
        [SerializeField] private bool _createOnAwake = true;
        [SerializeField] private Markable _markablePrefab;
        [SerializeField] private Transform _markableparent;
        private List<Markable> _markables;
        private int _quantityMarks;

        private List<Markable> Markables
        {
            get
            {
                if (_markables == null)
                {
                    _markables = new List<Markable>();
                }
                return _markables;
            }
        }
        public int QuantityMaks => _quantityMarks;

        private void Awake()
        {
            if (_createOnAwake)
            {
                SetQuantityMarks(_markablesQuantity);
            }
        }

        public void SetQuantityMarks(int quantity)
        {
            _quantityMarks = quantity;
            Clear();
            for (int markId = 0; markId < quantity; markId++)
            {
                Markable markable = CreateMarkable();
                markable.SetState(MarkableState.Unmarked);
            }
        }

        public void Mark(int quantity)
        {
            foreach (Markable markable in Markables)
            {
                if (quantity > 0)
                {
                    markable.SetState(MarkableState.Marked);
                    quantity--;
                }
                else
                {
                    markable.SetState(MarkableState.Unmarked);
                }
            }
        }

        public void Inactive()
        {
            foreach (Markable markable in Markables)
            {
                markable.SetState(MarkableState.Inactived);
            }
        }

        private void Clear()
        {
            foreach (Markable markable in Markables)
            {
                Destroy(markable.gameObject);
            }
            Markables.Clear();
        }

        private Markable CreateMarkable()
        {
            Markable markable = Instantiate(_markablePrefab, _markableparent);
            Markables.Add(markable);
            return markable;
        }
    }
}
