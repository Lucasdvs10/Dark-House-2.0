using System.Collections.Generic;
using Core_Scripts.SOSingletons;
using GameScripts.GameEvent;
using UnityEngine;

namespace Core_Scripts.PlayerSoundSystem {
    public class AudioManager : MonoBehaviour {
        [SerializeField] private SOBaseGameEvent _whenToEmitSoundEvent;
        [SerializeField] private SOVec3Singleton _playerDesiredPos;
        [SerializeField] private AudioSource _audioEmmiter;

        [SerializeField] private AudioClip[] _audiosArray;
        [SerializeField] private string[] _tileNamesArray;
        private Dictionary<string, AudioClip> _audioMap = new Dictionary<string, AudioClip>();

        private void Awake() {
            _whenToEmitSoundEvent.Subscribe(TeleportSoundEmmiterToDesiredPos);
            for (int i = 0; i < _audiosArray.Length; i++) {
                _audioMap.Add(_tileNamesArray[i], _audiosArray[i]);
            }
        }
        
        //todo: Ao invés de só injetar o áudio, criar uma fila e adicioná-lo na fila (dessa maneira, não precisaremos cortar nenhum áudio no meio)
        //todo: Criar um som padrão para tiles ainda não cadastrados
        public void PlaySoundEffect(string tileName) {
            _audioEmmiter.clip = _audioMap[tileName];
            _audioEmmiter.Play();
        }
        
        public void TeleportSoundEmmiterToDesiredPos() {
            _audioEmmiter.transform.position = _playerDesiredPos.Value;
        }

        public Dictionary<string, AudioClip> AudioMap => _audioMap;
    }
}