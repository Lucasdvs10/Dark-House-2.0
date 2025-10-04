using Core_Scripts.SOSingletons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core_Scripts {
    public class LoadNextScene : MonoBehaviour {
        public string NextSceneName;
        [SerializeField] private SOStringSingleton _soStringSingleton;
        
        public void LoadSceneOnSingleton() {
            SceneManager.LoadScene(_soStringSingleton.Value);
        }

        public void LoadScene() {
            SceneManager.LoadScene(NextSceneName);
        }
    }
}