using UnityEngine;

namespace Core_Scripts {
    public class ToggleTimeScale : MonoBehaviour {
        private bool _scaleIsZero;
        public void SetTimeScale() {
            if (_scaleIsZero) {
                Time.timeScale = 1;
                _scaleIsZero = false;
                print("Time scale é um");
            }
            else {
                Time.timeScale = 0;
                _scaleIsZero = true;
                print("Time scale é zero");
            }
        }

        private void OnEnable() {
            // Debug.Log("Mudar Time scale para 1");
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