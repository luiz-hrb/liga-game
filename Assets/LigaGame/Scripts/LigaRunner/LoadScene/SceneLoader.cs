using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LigaGame.LoadScene
{
    public sealed class SceneLoader : MonoBehaviour
    {
        private AsyncOperation _asyncLoadScene;
        private WaitForSeconds _waitBeforeLoadScene;
        private const float _timeBeforeLoadScene = 0.5f;
        private const float _minimumLoadProgress = 0.9f;
        
        public static SceneLoader Instance { get; private set; }
        public AsyncOperation AsyncLoadScene => _asyncLoadScene;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }

            Initialize();
        }

        private void Initialize()
        {
            DontDestroyOnLoad(gameObject);
            _waitBeforeLoadScene = new WaitForSeconds(_timeBeforeLoadScene);
        }

        public void ReloadThisScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadLevelAsync(ScenesIndex level)
        {
            StartCoroutine(LoadAsync((int)level));
        }

        private IEnumerator LoadAsync(int nextLevelId)
        {
            SceneManager.LoadScene((int)ScenesIndex.ASYNC_LOAD);
            yield return _waitBeforeLoadScene;

            _asyncLoadScene = SceneManager.LoadSceneAsync(nextLevelId);
            _asyncLoadScene.allowSceneActivation = false;

            while (_asyncLoadScene.progress < _minimumLoadProgress)
            {
                yield return null;
            }

            yield return _waitBeforeLoadScene;

            ActiveScene(_asyncLoadScene);

            yield return _waitBeforeLoadScene;
        }

        private void ActiveScene(AsyncOperation asyncLoad)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            asyncLoad.allowSceneActivation = true;
        }
    }
}
