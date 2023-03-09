using UnityEngine;

namespace Core_Scripts.BattleSystem.Timer {
    [RequireComponent(typeof(BattleTimer), typeof(AudioSource))]
    public class AudioTimer : MonoBehaviour {
        [SerializeField] private AudioClip[] _audiosArray;
        private AudioSource _audioSource;
        private BattleTimer _battleTimer;
        private void Awake() {
            _battleTimer = GetComponent<BattleTimer>();
            _audioSource = GetComponent<AudioSource>();
        }

        public void StartTimer() {
            _battleTimer.StartTimerCoroutine(_audiosArray.Length + 1);
        }

        public void PlayAudioFromArray(int index) {
            _audioSource.clip = _audiosArray[index];
            _audioSource.Play();
        }
        
    }
}