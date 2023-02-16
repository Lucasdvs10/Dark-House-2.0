using UnityEngine;

namespace Core_Scripts.GridSystem.MonoBehaviours {
    public class GridBehaviour : MonoBehaviour {
        public GridEntity GridEntity;
        public Vector2Int GridRowsAndColums;

        private void Start() {
            GridEntity = new GridEntity(GridRowsAndColums.x, GridRowsAndColums.y, transform.position);
        }

        private void OnDrawGizmos() {
            if (Application.isPlaying) {
                foreach (var cellTuple in GridEntity.Grid) {
                    Gizmos.DrawWireCube(cellTuple.cellWorldPos, Vector3.one);
                }
            }
        }
    }
}