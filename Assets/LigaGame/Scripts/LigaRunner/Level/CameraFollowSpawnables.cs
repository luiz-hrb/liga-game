using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LigaGame.Player;
using Cinemachine;

namespace LigaGame.Level
{
    public class CameraFollowSpawnables : MonoBehaviour
    {
        [SerializeField] private Spawner _spawner;
        [SerializeField] private CinemachineVirtualCamera _cinemachineCamera;

        private void Start()
        {
            if (_spawner != null)
            {
                _spawner.OnSpawn.AddListener(SetTarget);

                if (_spawner.LastObject != null)
                {
                    SetTarget(_spawner.LastObject);
                }
            }
        }

        private void OnDestroy()
        {
            if (_spawner != null)
            {
                _spawner.OnSpawn.AddListener(SetTarget);
            }
        }

        private void SetTarget(GameObject targetObject)
        {
            _cinemachineCamera.Follow = targetObject.transform;
        }
    }
}
