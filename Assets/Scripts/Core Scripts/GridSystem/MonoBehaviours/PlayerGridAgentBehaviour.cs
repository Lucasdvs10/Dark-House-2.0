using System.Collections;
using GameScripts.GameEvent;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core_Scripts.GridSystem.MonoBehaviours {
    public class PlayerGridAgentBehaviour : MonoBehaviour {
        public GridAgent GridAgent;
        public GridEntity GridEntity;
        public GridBehaviour GridBehaviour;
        private Vector2Int _gridDirection;
        
        [SerializeField] private float _speedCellsPerSecond;
        [SerializeField] private SOBaseGameEvent playerMoveCommandInvoked;

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
            GridAgent.MoveAgentToDirection(direction);
            transform.position = GridAgent.WorldPosition;
            playerMoveCommandInvoked.InvokeEvent();
        }
    }
}