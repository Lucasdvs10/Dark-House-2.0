using Core_Scripts.SOSingletons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core_Scripts {
    public class SetCurrentSceneIndex : MonoBehaviour {
        [SerializeField] private SOStringSingleton _soStringSingleton;

        private void Awake() {
            _soStringSingleton.Value = SceneManager.GetActiveScene().name;
        }

    }
}