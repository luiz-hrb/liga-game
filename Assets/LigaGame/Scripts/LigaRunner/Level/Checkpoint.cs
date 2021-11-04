using UnityEngine;
using UnityEngine.Events;
using LigaGame.Player;

namespace LigaGame.Level
{
    public class Checkpoint : MonoBehaviour
    {
        public UnityEvent OnCheckpointReached;

        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerController player = other.GetComponentInParent<PlayerController>();

            if (player != null)
            {
                OnCheckpointReached.Invoke();
            }
        }
    }
}
