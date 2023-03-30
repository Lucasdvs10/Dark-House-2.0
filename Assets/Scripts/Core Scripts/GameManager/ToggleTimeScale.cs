using UnityEngine;

namespace Core_Scripts {
    public class ToggleTimeScale : MonoBehaviour {
        private bool _scaleIsZero;
        public void SetTimeScale() {
            if (_scaleIsZero) {
                Time.timeScale = 1;
                _scaleIsZero = false;
            }
            else {
                Time.timeScale = 0;
                _scaleIsZero = true;
            }
        }
    }
}