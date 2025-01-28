using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DefaultNamespace {
    public class PlayTutorial : MonoBehaviour {
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private float _audioSpeed = 1;
        private AudioSource _audioSource;
        private UnityEvent _onButtonPressed;

        private void Awake() {
            _audioSource = GetComponentInParent<AudioSource>();
            _onButtonPressed = GetComponent<Button>().onClick;
        }

        private void OnEnable() {
            _onButtonPressed.AddListener(PlayAudio);
        }

        private void OnDisable() {
            _onButtonPressed.RemoveListener(PlayAudio);
        }

        public void PlayAudio() {
            if (_audioSource.clip == _audioClip) {
                _audioSource.Play();
                return;
            }
            
            var time = _audioSource.time;
            
            _audioSource.clip = _audioClip;
            _audioSource.time = time / _audioSpeed;
            
            _audioSource.Play();
        }
    }
}