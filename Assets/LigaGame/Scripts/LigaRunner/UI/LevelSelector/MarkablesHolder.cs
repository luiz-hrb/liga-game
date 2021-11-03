using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LigaGame.UI
{
    public class MarkablesHolder : MonoBehaviour
    {
        [SerializeField] private List<Markable> markables;

        public void Mark(int quantity)
        {
            foreach (Markable markable in markables)
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
            foreach (Markable markable in markables)
            {
                markable.SetState(MarkableState.Inactived);
            }
        }
    }
}
