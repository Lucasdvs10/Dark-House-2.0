using System;
using GameScripts.GameEvent;
using UnityEngine;

namespace Core_Scripts {
    [RequireComponent(typeof(AudioSource))]
    public class PlaySFXOnEvent : MonoBehaviour {
        private AudioSource _audioSource;

        public SOBaseGameEvent _eventKey;

        public AudioClip _audioClip;
        private void Awake() {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayClip() {
            _audioSource.clip = _audioClip;
            _audioSource.Play();
        }

        private void OnEnable() {
            _eventKey.Subscribe(PlayClip);
        }

        private void OnDisable() {
            _eventKey.Unsubscribe(PlayClip);
        }
    }
}