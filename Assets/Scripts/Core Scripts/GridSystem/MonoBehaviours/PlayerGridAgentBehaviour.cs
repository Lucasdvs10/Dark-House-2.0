using System.Collections;
using Core_Scripts.SOSingletons;
using GameScripts.GameEvent;
using UnityEngine;
using UnityEngine.Events;

namespace Core_Scripts.GridSystem.MonoBehaviours {
    public class PlayerGridAgentBehaviour : MonoBehaviour {
        public GridAgent GridAgent;
        public GridEntity GridEntity;
        public GridBehaviour GridBehaviour;
        [SerializeField] private float _speedCellsPerSecond;
        [SerializeField] private SOBaseGameEvent playerMoveCommandInvoked;
        [SerializeField] private SOVec3Singleton vec3Singleton;
        [SerializeField] private SOVec2IntSingleton _playerDirectionSingleton;
        [SerializeField] private SOVec2IntSingleton _playerPressedButtonSingleton;
        [SerializeField] private SOBaseGameEvent _pressedButtonEvent;
        [SerializeField] private SOBaseGameEvent _releasedButtonEvent;
        public UnityEvent<Vector2Int> OnHeadDirectionChangedEvent;


        private Vector2Int _gridDirection;
        private readonly Vector2Int[] _headDirections = {Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left};
        private int _currentDirection = 0;

        private void OnEnable() {
            _playerDirectionSingleton.Value = Vector2Int.zero;
            _pressedButtonEvent.Subscribe(ChangeHeadDirection);
            _pressedButtonEvent.Subscribe(MoveWhenPressToWalkFoward);
            _releasedButtonEvent.Subscribe(StopMovingCoroutines);
        }

        private void OnDisable() {
            _pressedButtonEvent.Unsubscribe(ChangeHeadDirection);
            _pressedButtonEvent.Unsubscribe(MoveWhenPressToWalkFoward);
            _releasedButtonEvent.Unsubscribe(StopMovingCoroutines);
            StopMovingCoroutines();
        }

        private void Start() {
            var thisPostion = transform.position;
            GridEntity = GridBehaviour.GridEntity;

            var gridPos = new Vector2Int(GridEntity.GetCellFromWorldPos(thisPostion).row,
                GridEntity.GetCellFromWorldPos(thisPostion).col);
            GridAgent = new GridAgent(GridEntity, gridPos);
            transform.position = GridAgent.WorldPosition;
        }

        private IEnumerator MoveCoroutine() { 
            while (true) {
                MoveAgentToDirection(_gridDirection);
                yield return new WaitForSeconds(1/_speedCellsPerSecond);
            }
        }
        
        public void MoveWhenPressToWalkFoward() { 
            if(_playerPressedButtonSingleton.Value == Vector2Int.left)
                StartCoroutine(MoveCoroutine());
        }

        public void ChangeHeadDirection() {
            if (_playerPressedButtonSingleton.Value == Vector2Int.up) {
                CurrentDirection++;
                OnHeadDirectionChangedEvent.Invoke(_playerPressedButtonSingleton.Value);
            }
            else if (_playerPressedButtonSingleton.Value == Vector2Int.down) {
                CurrentDirection--;
                OnHeadDirectionChangedEvent.Invoke(_playerPressedButtonSingleton.Value);
            }
            _gridDirection = _headDirections[_currentDirection];
        }

        public void StopMovingCoroutines() => StopAllCoroutines();
        
        public void MoveAgentToDirection(Vector2Int direction) { 
            GetPlayerDesiredPos(direction);

            GridAgent.MoveAgentToDirection(direction);
            transform.position = GridAgent.WorldPosition;
            _playerDirectionSingleton.Value = direction;

            playerMoveCommandInvoked.InvokeEvent();
        }

        public int CurrentDirection {
            get => _currentDirection;
            set {
                _currentDirection = value;
                
                if (_currentDirection < 0)
                    _currentDirection = _headDirections.Length - 1;
                else if (_currentDirection > _headDirections.Length - 1)
                    _currentDirection = 0;
            }
        }

        public void SetCanWalkFlag(bool newFlagValue) => GridAgent.CanWalk = newFlagValue;

        private void GetPlayerDesiredPos(Vector2Int direction) {
            var desiredCell = GridAgent.CurrentGridPosition + direction;
            vec3Singleton.Value = GridEntity.GetCell(desiredCell.x, desiredCell.y).cellWorldPos;
        }
    }
}