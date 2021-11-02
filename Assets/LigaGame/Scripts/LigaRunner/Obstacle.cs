using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LigaGame.Health;

namespace LigaGame
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private float _damage = 1f;

        private void OnCollisionEnter2D(Collision2D other)
        {
            HealthBehaviour health = other.collider.GetComponentInParent<HealthBehaviour>();

            if (health != null)
            {
                health.Damage(_damage);
            }
        }
    }
}
