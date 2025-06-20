﻿using UnityEngine;

namespace Core_Scripts.GridSystem {
    public class GridEntity {
        private (Vector2 cellWorldPos, bool walkable)[,] _grid;
        public readonly int RowsAmount;
        public readonly int ColumnsAmount;
        public readonly Vector2 GridEntityWorldPos;

        public Vector2 GetCellWorldPos(int row, int col) {
            int rowClamped = Mathf.Clamp(row, 0, RowsAmount - 1);
            int colClamped = Mathf.Clamp(col, 0, ColumnsAmount - 1);
            
            return _grid[rowClamped, colClamped].cellWorldPos;
        }

        public (Vector2 cellWorldPos, bool walkable) GetCell(int row, int col) {
            if (row < 0 || row >= RowsAmount) {
                Debug.Log("Essa célula não existe!");
            }
            if (col < 0 || col >= ColumnsAmount) {
                Debug.Log("Essa célula não existe!");
            }
            
            return _grid[row, col];
        }
        
        public void SetCellWalkableFlag(int row, int col, bool newFlagValue) {
            _grid[row, col].walkable = newFlagValue;
        }
        
        public void SetCellWalkableFlag(Vector2 cellWorldPos, bool newFlagValue) {
            (int row, int col, bool flag) = GetCellFromWorldPos(cellWorldPos);
            
            _grid[row, col].walkable = newFlagValue;
        }

        public (int row, int col, bool walkable) GetCellFromWorldPos(Vector2 worldPos) {
            var rowPos = Mathf.RoundToInt(GridEntityWorldPos.y - worldPos.y); //Nas linhas é ao contrário pq na grid, o y é crescente de cima pra baixo e no mundo é de baixo pra cima
            var colPos = Mathf.RoundToInt(worldPos.x - GridEntityWorldPos.x);

            return (rowPos, colPos, _grid[rowPos, colPos].walkable);
        }
        public GridEntity(int rows, int col, Vector2 worldPos) {
            _grid = new (Vector2 cellWorldPos, bool walkable)[rows,col];
            RowsAmount = rows;
            ColumnsAmount = col;
            GridEntityWorldPos = worldPos;

            for (int y = 0; y < RowsAmount; y++) {
                for (int x = 0; x < ColumnsAmount; x++) {
                    var xWorldPos = GridEntityWorldPos.x + x;
                    var yWorldPos = GridEntityWorldPos.y - y;

                    _grid[y,x] = (new Vector2(xWorldPos, yWorldPos), true);
                }
            }
        }

        public (Vector2 cellWorldPos, bool walkable)[,] Grid => _grid;
    }
}