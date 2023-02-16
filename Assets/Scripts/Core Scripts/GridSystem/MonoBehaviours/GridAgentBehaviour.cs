using UnityEngine;

namespace Core_Scripts.GridSystem.MonoBehaviours {
    public class GridAgentBehaviour : MonoBehaviour {
        public GridAgent GridAgent;
        public GridEntity GridEntity;
        public GridBehaviour GridBehaviour;

        private void Start() {
            GridEntity = GridBehaviour.GridEntity;

            var gridPos = new Vector2Int(GridEntity.GetCellFromWorldPos(transform.position).row,
                GridEntity.GetCellFromWorldPos(transform.position).col);
            
            GridAgent = new GridAgent(GridEntity, gridPos);
            
            print(GridAgent.CurrentGridPosition);
        }
    }
}