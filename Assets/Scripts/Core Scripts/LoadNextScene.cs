using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core_Scripts {
    public class LoadNextScene : MonoBehaviour {
        public string NextSceneName;

        public void LoadScene() {
            SceneManager.LoadScene(NextSceneName);
        }
    }
}