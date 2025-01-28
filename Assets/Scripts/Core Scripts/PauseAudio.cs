using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Core_Scripts {
    public class PauseAudio : MonoBehaviour {
        
        private AudioSource _audioSource;
        private UnityEvent _onButtonPressed;

        private void Awake() {
            _audioSource = GetComponentInParent<AudioSource>();
            _onButtonPressed = GetComponent<Button>().onClick;
        }

        private void OnEnable() {
            _onButtonPressed.AddListener(Pause);
        }

        private void OnDisable() {
            _onButtonPressed.RemoveListener(Pause);
        }

        public void Pause() {
            _audioSource.Pause();
        }
    }
}