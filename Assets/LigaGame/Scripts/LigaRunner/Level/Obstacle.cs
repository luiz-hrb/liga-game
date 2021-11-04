using UnityEngine;
using LigaGame.Health;

namespace LigaGame.Level
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private float _damage = 1f;

        private void OnCollisionEnter2D(Collision2D other)
        {
            HealthBehaviour health = other.collider.GetComponentInParent<HealthBehaviour>();

            if (health != null)
            {
                health.ChangeHealth(-_damage);
            }
        }
    }
}
