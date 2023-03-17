using Core_Scripts.SOSingletons;
using GameScripts.GameEvent;
using UnityEngine;
using UnityEngine.Events;

namespace Core_Scripts.KeySystem {
    public class Key : MonoBehaviour{
        [SerializeField] private SOBaseGameEvent _playerMoveEvent;
        [SerializeField] private SOVec3Singleton _playerPositionSingleton;
        [SerializeField] private SOBaseGameEvent _keyHasBeenColectedEvent;
        public UnityEvent _keyColectedLocalEvent;

        private void OnEnable() {
            _playerMoveEvent.Subscribe(CheckIfHasBeenColected);
        }

        private void OnDisable() {
            _playerMoveEvent.Unsubscribe(CheckIfHasBeenColected);
        }

        private void CheckIfHasBeenColected() {
            var playerPosition = _playerPositionSingleton.Value;

            if (playerPosition == transform.position) {
                _keyHasBeenColectedEvent.InvokeEvent();
                _keyColectedLocalEvent.Invoke();
            }
        }
    }
}