using UnityEngine;

namespace Core_Scripts {
    [RequireComponent(typeof(AudioSource))]
    public class LevelTimerUI : MonoBehaviour {
        [SerializeField] private int _delayInSeconds;
        [SerializeField] private AudioClip _audioClip;
        
        private AudioSource _audioSource;
        
        private void Awake() {
            _audioSource = GetComponent<AudioSource>();
        }
        
        public void PlaySound() {
            _audioSource.clip = _audioClip;
            _audioSource.PlayDelayed(_delayInSeconds);
        }
    }
}