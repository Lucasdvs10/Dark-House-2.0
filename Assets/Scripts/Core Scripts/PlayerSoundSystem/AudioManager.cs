using System;
using System.Collections;
using System.Collections.Generic;
using Core_Scripts.SOSingletons;
using GameScripts.GameEvent;
using UnityEngine;

namespace Core_Scripts.PlayerSoundSystem {
    public class AudioManager : MonoBehaviour {
        [SerializeField] private SOBaseGameEvent _whenToEmitSoundEvent;
        [SerializeField] private SOVec3Singleton _playerDesiredPos;
        [SerializeField] private AudioSource _audioEmmiter;
        private Queue<AudioClip> _audioClipsQueue = new Queue<AudioClip>();

        #region Dicionário de audios

        [Header("Dicionários de áudio")]
        [Header("Chave")]
        [SerializeField] private string[] _tileNamesArray;
        [Header("Valor")]
        [SerializeField] private AudioClip[] _audiosArray;

        [SerializeField] private AudioClip _defaultAudioClip;
        #endregion
        
        private Dictionary<string, AudioClip> _audioMap = new Dictionary<string, AudioClip>();

        private void Awake() {
            for (int i = 0; i < _audiosArray.Length; i++) {
                _audioMap.Add(_tileNamesArray[i], _audiosArray[i]);
            }
        }

        private void OnEnable() {
            _whenToEmitSoundEvent.Subscribe(TeleportSoundEmmiterToDesiredPos);
        }

        private void OnDisable() {
            _whenToEmitSoundEvent.Unsubscribe(TeleportSoundEmmiterToDesiredPos);
        }

        private void Update() {
            if (_audioClipsQueue.Count > 0 && !_audioEmmiter.isPlaying) {
                StartPlayClipsInQueueCoroutine();
            }
        }

        public void AddToQueueAndStartPlayAllClipsCoroutine(string tileName) {
            AddClipToQueue(tileName);
        }

        [ContextMenu("Start Coroutine")]
        public void StartPlayClipsInQueueCoroutine() {
            StartCoroutine(PlayAllClipsInQueueCoroutine());
        }
        private IEnumerator PlayAllClipsInQueueCoroutine() {
            while (_audioClipsQueue.Count > 0) {
                var nextClip = _audioClipsQueue.Dequeue();
                _audioEmmiter.clip = nextClip;
                _audioEmmiter.Play();
                yield return new WaitUntil(() => !_audioEmmiter.isPlaying);
                _audioEmmiter.clip = null;
            }
        }

        public void AddClipToQueue(string tileName) {
            if(!_audioMap.TryGetValue(tileName, out var clip))
                _audioClipsQueue.Enqueue(_defaultAudioClip);
            _audioClipsQueue.Enqueue(clip);
        }
        
        public void StopQueueAndPlaySoundEffect(string tileName) {
            _audioEmmiter.clip = _audioMap[tileName];
            _audioEmmiter.Play();
        }
        public void TeleportSoundEmmiterToDesiredPos() {
            _audioEmmiter.transform.position = _playerDesiredPos.Value;
        }

        public Dictionary<string, AudioClip> AudioMap => _audioMap;
    }
}