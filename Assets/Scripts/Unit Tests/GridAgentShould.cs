using Core_Scripts.GridSystem;
using NUnit.Framework;
using UnityEngine;

namespace Unit_Tests {
    public class GridAgentShould {
        private GridAgent _gridAgent;
        private GridEntity _grid;

        [SetUp]
        public void SetUp_all_tests() {
            _grid = new GridEntity(3,3, new Vector2(5,5));
            _gridAgent = new GridAgent(_grid, new Vector2Int(2,2));
        }

        [Test]
        public void Return_Grid_Pos_As_VectInt_2x_2y() {
            var gridAgentInitialGridPos = _gridAgent.CurrentGridPosition;
            
            Assert.AreEqual(new Vector2Int(2,2), gridAgentInitialGridPos);
        }

        [Test]
        public void Return_World_Pos_As_7x_7y() {
            var agentWorldPos = _gridAgent.WorldPosition;
            
            Assert.AreEqual(new Vector2(7,7), agentWorldPos);
        }
    }
}

public class GridAgent {
    private Vector2Int _currentGridPosition;
    private GridEntity _gridEntity;
    private Vector2 _worldPosition;

    public GridAgent(GridEntity gridEntity, Vector2Int initialGridPosition) {
        _currentGridPosition = initialGridPosition;
        _gridEntity = gridEntity;

        _worldPosition = _gridEntity.GetCellWorldPos(initialGridPosition.x, initialGridPosition.y);
    }

    public Vector2Int CurrentGridPosition => _currentGridPosition;

    public Vector2 WorldPosition => _worldPosition;

    public GridEntity GridEntity => _gridEntity;
}