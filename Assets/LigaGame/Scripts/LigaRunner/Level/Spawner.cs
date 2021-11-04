using UnityEngine;
using UnityEngine.Events;

namespace LigaGame.Level
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        private GameObject _lastObject;

        public GameObject LastObject => _lastObject;

        public UnityEvent<GameObject> OnSpawn;

        public GameObject Spawn()
        {
            _lastObject = Instantiate(_prefab);
            _lastObject.transform.position = transform.position;
            OnSpawn.Invoke(_lastObject);
            return _lastObject;
        }
    }
}
