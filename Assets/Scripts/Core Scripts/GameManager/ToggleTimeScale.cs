using System;
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

        private void OnEnable() {
            print("Mudar Time scale para 1");
            Time.timeScale = 1;
        }

        private void OnDisable() {
            print("Mudar Time scale para 1");
            Time.timeScale = 1;
        }
    }
}