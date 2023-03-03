using UnityEngine;

namespace Core_Scripts.GridSystem {
    public class GridAgent {
        private Vector2Int _currentGridPosition;
        private GridEntity _gridEntity;
        private Vector2 _worldPosition;
        public bool CanWalk = true;

        public void MoveAgentToDirection(Vector2Int direction) {
            if(!CanWalk)
                return;
            
            SetGridPos(CurrentGridPosition.x + direction.x, CurrentGridPosition.y + direction.y);
        }
    
        public void SetGridPos(int newRowPos, int newColPos) {
            var newRowPosClamped = Mathf.Clamp(newRowPos, 0, _gridEntity.RowsAmount - 1);
            var newColPosClamped = Mathf.Clamp(newColPos, 0, _gridEntity.ColumnsAmount - 1);
        
            if(!_gridEntity.GetCell(newRowPosClamped, newColPosClamped).walkable)
                return;
        
            _worldPosition = _gridEntity.GetCellWorldPos(newRowPosClamped, newColPosClamped);
            _currentGridPosition = new Vector2Int(newRowPosClamped, newColPosClamped);
        }
    
        public GridAgent(GridEntity gridEntity, Vector2Int initialGridPosition) {
            _currentGridPosition = initialGridPosition;
            _gridEntity = gridEntity;

            _worldPosition = _gridEntity.GetCellWorldPos(initialGridPosition.x, initialGridPosition.y);
        }

        public Vector2Int CurrentGridPosition => _currentGridPosition;

        public Vector2 WorldPosition => _worldPosition;

        public GridEntity GridEntity => _gridEntity;
    }
}