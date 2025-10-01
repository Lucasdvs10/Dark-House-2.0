using Core_Scripts.SOSingletons;
using GameScripts.GameEvent;
using UnityEngine;
using UnityEngine.Events;

namespace Core_Scripts.KeySystem {
    public class Level3Key : MonoBehaviour{
        [SerializeField] private SOBaseGameEvent _playerMoveEvent;
        [SerializeField] private SOVec3Singleton _playerPositionSingleton;
        [SerializeField] private SOIntGameEvent _keyHasBeenColectedEvent;
        [SerializeField] private int _collectDistanceInCells = 1;
        [SerializeField] private int _cellDistanceThreashold = 1;
        [SerializeField] private SOBaseGameEvent _wrongKeyCollectedEvent;
        private int _id;
        public UnityEvent _keyColectedLocalEvent;
        private bool _isActive = true;

        private const float _cellSize = 2.25f;
        private void OnEnable() {
            _playerMoveEvent.Subscribe(CheckIfHasBeenColected);
            _wrongKeyCollectedEvent.Subscribe(DeactivateKey);
        }

        private void OnDisable() {
            _playerMoveEvent.Unsubscribe(CheckIfHasBeenColected);
            _wrongKeyCollectedEvent.Unsubscribe(DeactivateKey);
        }

        public void SetId(int id) {
            _id = id;
        }

        private void CheckIfHasBeenColected() {
            var playerPosition = _playerPositionSingleton.Value;

            if ((playerPosition - transform.position).sqrMagnitude <= _cellSize * _collectDistanceInCells) { //Fiz desse jeito para que a chave tenha uma hitbox maior
                if (_isActive) {
                    _keyHasBeenColectedEvent.InvokeEvent(_id);
                    _keyColectedLocalEvent.Invoke();
                }
            }
            else if((playerPosition - transform.position).sqrMagnitude > _cellSize * _collectDistanceInCells + _cellDistanceThreashold && !_isActive) {
                _isActive = true;
            }
        }
        
        public void DeactivateKey() => _isActive = false;
    }
}