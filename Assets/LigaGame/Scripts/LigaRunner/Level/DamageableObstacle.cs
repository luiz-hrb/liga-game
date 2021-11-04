using UnityEngine;
using LigaGame.Health;

namespace LigaGame.Level
{
    public class DamageableObstacle : MonoBehaviour
    {
        [SerializeField] private float _damage = 1f;

        private void OnCollisionEnter2D(Collision2D other)
        {
            HealthBehaviour healthBehaviour = other.collider.GetComponentInParent<HealthBehaviour>();

            if (healthBehaviour != null)
            {
                healthBehaviour.ChangeHealth(-_damage);
            }
        }
    }
}
