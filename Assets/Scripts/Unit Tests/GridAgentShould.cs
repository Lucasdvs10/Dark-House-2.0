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
        
        // [Test]
        // public void Return_World_Pos_As
    }
}

public class GridAgent {
    private Vector2Int _currentGridPosition;
    private GridEntity _gridEntity;

    public GridAgent(GridEntity gridEntity, Vector2Int initialGridPosition) {
        _currentGridPosition = initialGridPosition;
        _gridEntity = gridEntity;
    }

    public Vector2Int CurrentGridPosition => _currentGridPosition;

    public GridEntity GridEntity => _gridEntity;
}