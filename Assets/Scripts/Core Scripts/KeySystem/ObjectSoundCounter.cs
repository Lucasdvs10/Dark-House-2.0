using GameScripts.GameEvent;
using UnityEngine;

namespace Core_Scripts.KeySystem {
    [RequireComponent(typeof(IManagerCounter), typeof(AudioSource))]
    public class ObjectSoundCounter : MonoBehaviour {
        [SerializeField] private SOBaseGameEvent _keyCounterPressedEvent;
        [SerializeField] private AudioClip[] _audioClipsArray;
        private IManagerCounter _managerCounter;
        private AudioSource _audioSource;

        private void Awake() {
            _managerCounter = GetComponent<IManagerCounter>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable() {
            _keyCounterPressedEvent.Subscribe(ShowKeysColectedAmount);
        }

        private void OnDisable() {
            _keyCounterPressedEvent.Unsubscribe(ShowKeysColectedAmount);
        }
        
        public void ShowKeysColectedAmount() {
            print($"Eu já coletei {_managerCounter.ObjectsColectedAmount} chaves!");
            _audioSource.PlayOneShot(_audioClipsArray[_managerCounter.ObjectsColectedAmount]);
        }

    }
}