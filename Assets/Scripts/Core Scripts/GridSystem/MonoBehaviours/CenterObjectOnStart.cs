using UnityEngine;

namespace Core_Scripts.GridSystem.MonoBehaviours {
    public class CenterObjectOnStart : MonoBehaviour {
        private GridAgent _gridAgent;
        private GridEntity _gridEntity;
        private GridBehaviour _gridBehaviour;

        private void Start() {
            _gridBehaviour = FindObjectOfType<GridBehaviour>();
            
            var thisPostion = transform.position;
            _gridEntity = _gridBehaviour.GridEntity;

            var gridPos = new Vector2Int(_gridEntity.GetCellFromWorldPos(thisPostion).row,
                _gridEntity.GetCellFromWorldPos(thisPostion).col);
            _gridAgent = new GridAgent(_gridEntity, gridPos);
            transform.position = _gridAgent.WorldPosition;
        }
    }
}