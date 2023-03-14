using System;
using System.Collections;
using UnityEngine;

namespace Core_Scripts.GhostIASystem {
    [RequireComponent(typeof(AudioSource))]
    public class PlayAudioQueue : MonoBehaviour {
        public AudioClip[] AudioClipsArray;
        private int _currentIndex = 0;
        private AudioSource _audioSource;

        private void Awake() {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable() {
            StartPlayAudioCoroutine();
        }

        private void OnDisable() {
            StopPlayAudioCoroutine();
        }

        public void StartPlayAudioCoroutine() => StartCoroutine(PlayAudioQueueCoroutine());
        public void StopPlayAudioCoroutine() => StopAllCoroutines();
        
        private IEnumerator PlayAudioQueueCoroutine() {
            while (true) {
                _audioSource.clip = AudioClipsArray[_currentIndex];
                _audioSource.Play();

                yield return new WaitUntil(() => !_audioSource.isPlaying);
                
                AddOneToIndex();
            }
        }

        private void AddOneToIndex() {
            _currentIndex++;

            if (_currentIndex >= AudioClipsArray.Length) {
                _currentIndex = 0;
            }
        }
    }
}