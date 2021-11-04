using UnityEngine;
using LigaGame.Player;

namespace LigaGame.LoadScene
{
    public class PlayerAnimationLoadScene : MonoBehaviour
    {
        [SerializeField] private float _lerpAmount = 1f;
        [SerializeField] private PlayerView _player;
        [SerializeField] private Transform _startPosition;
        [SerializeField] private Transform _endPosition;
        private float _currentPosition;
        private float _targetPosition;

        private void Awake()
        {
            _player.Move(1f);
        }

        void Update()
        {
            AsyncOperation loadSceneOperation = SceneLoader.Instance.AsyncLoadScene;
            if (loadSceneOperation != null)
            {
                SetPlayerTargetPosition(loadSceneOperation.progress);
            }

            _currentPosition = Mathf.Lerp(_currentPosition, _targetPosition, _lerpAmount * Time.deltaTime);
            _player.transform.position = Vector3.Lerp(_startPosition.position, _endPosition.position, _currentPosition);
        }

        private void SetPlayerTargetPosition(float position)
        {
            _targetPosition = position;
        }
    }
}
