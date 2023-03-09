using System;
using System.Collections;
using System.Collections.Generic;
using Core_Scripts.ExtentionMethods;
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
        
        private Dictionary<string, List<AudioClip>> _audioMap = new Dictionary<string, List<AudioClip>>();

        private void Awake() {
            for (int i = 0; i < _audiosArray.Length; i++) {
                var currentKey = _tileNamesArray[i];
                if (!_audioMap.ContainsKey(currentKey)) {
                    _audioMap.Add(currentKey, new List<AudioClip>(){_audiosArray[i]});
                    continue;
                }
                _audioMap[currentKey].Add(_audiosArray[i]);
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
            if(!_audioMap.TryGetValue(tileName, out var clipsList))
                _audioClipsQueue.Enqueue(_defaultAudioClip);
            _audioClipsQueue.Enqueue(clipsList.GetRandomClipFromList());
        }
        
        
        public void TeleportSoundEmmiterToDesiredPos() {
            _audioEmmiter.transform.position = _playerDesiredPos.Value;
        }

        public Dictionary<string, List<AudioClip>> AudioMap => _audioMap;
    }
}