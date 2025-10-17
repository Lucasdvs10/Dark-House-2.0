using UnityEngine;

namespace Core_Scripts {
    public class ToggleTimeScale : MonoBehaviour {
        private bool _scaleIsZero;
        public void SetTimeScale() {
            if (_scaleIsZero) {
                // Debug.Log("Mudar Time scale para 1");
                Time.timeScale = 1;
                _scaleIsZero = false;
            }
            else {
                // Debug.Log("Mudar Time scale para 0");
                Time.timeScale = 0;
                _scaleIsZero = true;
            }
        }

        private void OnEnable() {
            Time.timeScale = 1;
            _scaleIsZero = false;
        }

        private void OnDisable() {
            // Debug.Log("Mudar Time scale para 1");
            Time.timeScale = 1;
            _scaleIsZero = false;
        }
    }
}