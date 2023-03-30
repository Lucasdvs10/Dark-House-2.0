using GameScripts.GameEvent;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core_Scripts {
    public class SceneLoaderManager : MonoBehaviour {
        public string SceneName;
        public bool AditiveMode;
        public SOBaseGameEvent _eventToListen;
        private bool _isSceneLoaded;

        private void OnEnable() {
            _eventToListen.Subscribe(LoadScene);
        }

        private void OnDisable() {
            _eventToListen.Unsubscribe(LoadScene);
        }

        public void LoadScene() {
            if (_isSceneLoaded) {
                SceneManager.UnloadSceneAsync(SceneName);
                _isSceneLoaded = false;
                return;
            }
            
            if (AditiveMode) {
                SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
                _isSceneLoaded = true;
            }
            else {
                SceneManager.LoadScene(SceneName);
                _isSceneLoaded = true;
            }
        }

    }
}