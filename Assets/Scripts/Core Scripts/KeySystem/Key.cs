using Core_Scripts.SOSingletons;
using GameScripts.GameEvent;
using UnityEngine;
using UnityEngine.Events;

namespace Core_Scripts.KeySystem {
    public class Key : MonoBehaviour{
        [SerializeField] private SOBaseGameEvent _playerMoveEvent;
        [SerializeField] private SOVec3Singleton _playerPositionSingleton;
        [SerializeField] private SOBaseGameEvent _keyHasBeenColectedEvent;
        [SerializeField] private int _collectDistanceInCells = 1;
        public UnityEvent _keyColectedLocalEvent;

        private const float _cellSize = 2.25f;
        private void OnEnable() {
            _playerMoveEvent.Subscribe(CheckIfHasBeenColected);
        }

        private void OnDisable() {
            _playerMoveEvent.Unsubscribe(CheckIfHasBeenColected);
        }
        
        

        private void CheckIfHasBeenColected() {
            var playerPosition = _playerPositionSingleton.Value;

            if ((playerPosition - transform.position).sqrMagnitude <= _cellSize * _collectDistanceInCells) { //Fiz desse jeito para que a chave tenha uma hitbox maior
                _keyHasBeenColectedEvent.InvokeEvent();
                _keyColectedLocalEvent.Invoke();
            }
        }
    }
}