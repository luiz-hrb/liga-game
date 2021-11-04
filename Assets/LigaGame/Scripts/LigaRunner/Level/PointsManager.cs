using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LigaGame.PowerUps;

namespace LigaGame.Level
{
    public class PointsManager : MonoBehaviour
    {
        [SerializeField] private PowerUp[] _pointsToCollect;
        [SerializeField] private LevelManager _levelManager;
        
        private void Awake()
        {
            foreach (PowerUps.PowerUp point in _pointsToCollect)
            {
                point.OnTake.AddListener(OnPointCollected);
            }
        }

        private void OnPointCollected()
        {
            _levelManager.AddPoints(1);
        }
    }
}
