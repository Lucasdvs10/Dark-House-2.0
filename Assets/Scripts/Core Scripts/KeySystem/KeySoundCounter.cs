using System.Collections;
using GameScripts.GameEvent;
using UnityEngine;

namespace Core_Scripts.KeySystem {
    [RequireComponent(typeof(KeyManager), typeof(AudioSource))]
    public class KeySoundCounter : MonoBehaviour {
        [SerializeField] private SOBaseGameEvent _keyCounterPressedEvent;
        [SerializeField] private AudioClip[] _audioClipsArray;
        private KeyManager _keyManager;
        private AudioSource _audioSource;
        private int _currentIndex;

        private void Awake() {
            _keyManager = GetComponent<KeyManager>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable() {
            _keyCounterPressedEvent.Subscribe(ShowKeysColectedAmount);
        }

        private void OnDisable() {
            _keyCounterPressedEvent.Unsubscribe(ShowKeysColectedAmount);
        }

        public void ShowKeysColectedAmount() {
            print($"Eu já coletei {_keyManager.KeysColectedAmount} chaves!");
            StartCoroutine(PlayAudiosCoroutine(_keyManager.KeysColectedAmount));
        }

        private IEnumerator PlayAudiosCoroutine(int audiosToPlay) {
            int audiosPlayed = 0;
            while (audiosPlayed < audiosToPlay) {
                _audioSource.clip = _audioClipsArray[_currentIndex];
                _audioSource.Play();
                
                yield return new WaitUntil(() => !_audioSource.isPlaying);

                audiosPlayed++;
                AddOneToIndex();
            }
        }
        
        
        private void AddOneToIndex() {
            _currentIndex++;

            if (_currentIndex >= _audioClipsArray.Length) {
                _currentIndex = 0;
            }
        }
    }
}