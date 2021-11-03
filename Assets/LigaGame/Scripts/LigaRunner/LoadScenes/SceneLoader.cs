using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LigaGame.LoadScenes
{
    public sealed class SceneLoader : MonoBehaviour
    {
        private AsyncOperation _asyncLoadScene;
        private WaitForSeconds _waitBeforeLoadScene;
        private static SceneLoader _instance;
        public static SceneLoader Instance
        {
            get => _instance;
            private set => _instance = value;
        }

        private void Start()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }

            Setup();
        }

        private void Setup()
        {
            DontDestroyOnLoad(gameObject);
            _waitBeforeLoadScene = new WaitForSeconds(1f);
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

            while (_asyncLoadScene.progress < 0.9f)
            {
                yield return null;
            }

            yield return _waitBeforeLoadScene;

            ActiveScene(SceneLoader.Instance._asyncLoadScene);

            yield return _waitBeforeLoadScene;
        }

        private void ActiveScene(AsyncOperation asyncLoad)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            asyncLoad.allowSceneActivation = true;
        }
    }
}