using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ScenesIndex
{
    ASYNC_LOAD = 0,
    MENU = 1,
    GAMEPLAY = 2,
}

namespace LigaGame.LoadScenes
{
    public sealed class LevelLoader : MonoBehaviour
    {
        private AsyncOperation _asyncLoadScene;
        private WaitForSeconds _waitBeforeLoadScene;
        public static LevelLoader _instance;
        public static LevelLoader Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject(nameof(LevelLoader)).AddComponent<LevelLoader>();
                }
                return _instance;
            }
            private set => _instance = value;
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _waitBeforeLoadScene = new WaitForSeconds(1f);
        }

        public void LoadLevelAsync(ScenesIndex level)
        {
            StartCoroutine(LoadAsync((int)level));
        }

        private IEnumerator LoadAsync(int nextLevel)
        {
            SceneManager.LoadScene((int)ScenesIndex.ASYNC_LOAD);

            yield return _waitBeforeLoadScene;

            _asyncLoadScene = SceneManager.LoadSceneAsync(nextLevel);
            _asyncLoadScene.allowSceneActivation = false;

            while (_asyncLoadScene.progress < 0.9f)
            {
                yield return null;
            }

            yield return _waitBeforeLoadScene;

            ActiveScene(LevelLoader.Instance._asyncLoadScene);

            yield return _waitBeforeLoadScene;
        }

        private void ActiveScene(AsyncOperation asyncLoad)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            asyncLoad.allowSceneActivation = true;
        }
    }
}
