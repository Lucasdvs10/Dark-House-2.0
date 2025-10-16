using UnityEngine;

namespace Core_Scripts {
    public class PauseObject : MonoBehaviour {
        [SerializeField] private Behaviour[] _monoBehavioursToPauseArray;
        private bool _isPaused;

        public void Pause() {
            foreach (var behaviour in _monoBehavioursToPauseArray) {
                behaviour.enabled = false;
            }

            _isPaused = true;
        }
        
        public void Resume() {
            foreach (var behaviour in _monoBehavioursToPauseArray) {
                behaviour.enabled = true;
            }

            _isPaused = false;
        }

        public void TogglePauseAndResume() {
            if (_isPaused) {
                Resume();
                // print("Despausando");
                return;
            }

            if (!_isPaused) {
                Pause();
                // print("Pausando");
            }
            
        }
    }
}