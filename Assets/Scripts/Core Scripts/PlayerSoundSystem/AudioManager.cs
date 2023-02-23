using System.Collections.Generic;
using Core_Scripts.SOSingletons;
using GameScripts.GameEvent;
using UnityEngine;

namespace Core_Scripts.PlayerSoundSystem {
    public class AudioManager : MonoBehaviour {
        [SerializeField] private SOBaseGameEvent _whenToEmitSoundEvent;
        [SerializeField] private SOVec3Singleton _playerDesiredPos;
        [SerializeField] private AudioSource _audioEmmiter;
        private Dictionary<Vector2Int, string> _audioMap;

        private void Awake() {
            _whenToEmitSoundEvent.Subscribe(TeleportSoudEmmiterToDesiredPos);
        }

        public void TeleportSoudEmmiterToDesiredPos() {
            _audioEmmiter.transform.position = _playerDesiredPos.Value;
        }

        public Dictionary<Vector2Int, string> AudioMap => _audioMap;
    }
}