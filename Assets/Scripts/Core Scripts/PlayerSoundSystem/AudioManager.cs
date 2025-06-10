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
        [SerializeField] private GameObject _audioEmitterPrefab;
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

        public void AddClipToQueue(string tileName) {
            if (!_audioMap.TryGetValue(tileName, out var clipsList)) {
                _audioClipsQueue.Enqueue(_defaultAudioClip);
                print("Entra aqui?");
            }
            _audioClipsQueue.Enqueue(clipsList.GetRandomClipFromList());
        }

        
        private bool _isPlayingAWallSound = false;
        public void InstantiateEmitterAndPlay() {
            var gameObject = Instantiate(_audioEmitterPrefab, _playerDesiredPos.Value, Quaternion.identity);
            var audioSource = gameObject.GetComponent<AudioSource>();
            var clip = _audioClipsQueue.Dequeue();
            
            //Fiz desse jeito para que apenas alguns sons sejam tocados um de cada vez
            //Há certos sons que não ficam ruins quando tocam ao mesmo tempo
            //Claro que não é a melhor solução em termos de código, mas fiz desse jeito para ganhar tempo
            //Se surgir a necessidade,a gente refatora essa lógica toda
            
            if (clip.name.StartsWith("Cadeira") || clip.name.StartsWith("Bater_") || clip.name.StartsWith("Gangorra")) {
                if (!_isPlayingAWallSound) {
                    StartCoroutine(PlayWallSound(audioSource, clip));
                }
            }
            else {
                audioSource.clip = clip;
                audioSource.Play();
            }
            Destroy(gameObject, clip.length + 0.2f);
        }
        
        private IEnumerator PlayWallSound(AudioSource audioSource, AudioClip clip) {
            _isPlayingAWallSound = true;
            audioSource.clip = clip;
            audioSource.Play();
            yield return new WaitForSeconds(clip.length);
            Destroy(audioSource.gameObject);
            _isPlayingAWallSound = false;
        }
    }
}