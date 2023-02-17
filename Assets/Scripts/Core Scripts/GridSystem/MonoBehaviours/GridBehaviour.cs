using GameScripts.GameEvent;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core_Scripts.GridSystem.MonoBehaviours {
    public class GridBehaviour : MonoBehaviour {
        public GridEntity GridEntity;
        public Vector2Int GridRowsAndColums;
        public Tilemap UnwalkableLayer;

        private void Awake() {
            GridEntity = new GridEntity(GridRowsAndColums.x, GridRowsAndColums.y, transform.position);
        }

        private void Start() {
            foreach (var cellTuple in GridEntity.Grid) {
                var tilePos = UnwalkableLayer.WorldToCell(cellTuple.cellWorldPos);
                if (UnwalkableLayer.GetTile(tilePos)) {
                    var cellAtGridPos = GridEntity.GetCellFromWorldPos(cellTuple.cellWorldPos);
                    
                    GridEntity.SetCellWalkableFlag(cellAtGridPos.row, cellAtGridPos.col, false);
                }
            }
        }

        public void EuTeAmoLaura() {
            print("Eu te amo, laura");
        }
        private void OnDrawGizmos() {
            if (Application.isPlaying) {
                foreach (var cellTuple in GridEntity.Grid) {
                    Gizmos.color = Color.white;
                    Gizmos.DrawWireCube(cellTuple.cellWorldPos, Vector3.one);

                    if (!GridEntity.GetCellFromWorldPos(cellTuple.cellWorldPos).walkable) {
                        Gizmos.color = new Color(0,0,0,0.33f);
                        Gizmos.DrawCube(cellTuple.cellWorldPos, Vector3.one);
                    }
                }
            }
        }
    }
}