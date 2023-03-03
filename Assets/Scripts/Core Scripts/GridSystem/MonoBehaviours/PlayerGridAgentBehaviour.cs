using System.Collections;
using Core_Scripts.SOSingletons;
using GameScripts.GameEvent;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core_Scripts.GridSystem.MonoBehaviours {
    public class PlayerGridAgentBehaviour : MonoBehaviour {
        public GridAgent GridAgent;
        public GridEntity GridEntity;
        public GridBehaviour GridBehaviour;
        [SerializeField] private float _speedCellsPerSecond;
        [SerializeField] private SOBaseGameEvent playerMoveCommandInvoked;
        [SerializeField] private SOVec3Singleton vec3Singleton;
        [SerializeField] private SOVec2IntSingleton _playerDirectionSingleton;
        
        private Vector2Int _gridDirection;

        private void OnEnable() {
            _playerDirectionSingleton.Value = Vector2Int.zero;
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
        
        public void ReadInputAndMove(InputAction.CallbackContext ctx) {
            
            if(ctx.performed) {
                var directionRed = ctx.ReadValue<Vector2>();
                _gridDirection = new Vector2Int((int) -directionRed.y, (int)directionRed.x);
                
                if(_gridDirection != Vector2Int.zero) 
                    StartCoroutine(MoveCoroutine());
            }

            if (_gridDirection == Vector2Int.zero) {
                StopAllCoroutines();
            }
        }
        public void MoveAgentToDirection(Vector2Int direction) {
            GetPlayerDesiredPos(direction);

            GridAgent.MoveAgentToDirection(direction);
            transform.position = GridAgent.WorldPosition;
            _playerDirectionSingleton.Value = direction;

            playerMoveCommandInvoked.InvokeEvent();
        }

        public void SetCanWalkFlag(bool newFlagValue) => GridAgent.CanWalk = newFlagValue;

        private void GetPlayerDesiredPos(Vector2Int direction) {
            var desiredCell = GridAgent.CurrentGridPosition + direction;
            vec3Singleton.Value = GridEntity.GetCell(desiredCell.x, desiredCell.y).cellWorldPos;
        }
    }
}