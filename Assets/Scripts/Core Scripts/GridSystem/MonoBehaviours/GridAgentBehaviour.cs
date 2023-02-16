using UnityEngine;

namespace Core_Scripts.GridSystem.MonoBehaviours {
    public class GridAgentBehaviour : MonoBehaviour {
        public GridAgent GridAgent;
        public GridEntity GridEntity;
        public GridBehaviour GridBehaviour;

        private void Start() {
            var thisPostion = transform.position;
            GridEntity = GridBehaviour.GridEntity;

            var gridPos = new Vector2Int(GridEntity.GetCellFromWorldPos(thisPostion).row,
                GridEntity.GetCellFromWorldPos(thisPostion).col);
            GridAgent = new GridAgent(GridEntity, gridPos);
            transform.position = GridAgent.WorldPosition;
        }
        
        public void MoveAgentToDirection(Vector2Int direction) {
            GridAgent.MoveAgentToDirection(direction);
            transform.position = GridAgent.WorldPosition;
        }
    }
}