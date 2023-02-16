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
        public void Return_World_Pos_As_7x_3y() {
            var agentWorldPos = _gridAgent.WorldPosition;
            
            Assert.AreEqual(new Vector2(7,3), agentWorldPos);
        }

        [Test]
        public void Teleport_Agent_To_0_0() {
            _gridAgent.SetGridPos(0, 0);
            
            Assert.AreEqual(_grid.GetCellWorldPos(0,0), _gridAgent.WorldPosition);
            Assert.AreEqual(new Vector2Int(0,0), _gridAgent.CurrentGridPosition);
        }

        [Test]
        public void Teleport_Agent_To_1_2() {
            _gridAgent.SetGridPos(1, 6);
            
            Assert.AreEqual(_grid.GetCellWorldPos(1,2), _gridAgent.WorldPosition);
            Assert.AreEqual(new Vector2Int(1,2), _gridAgent.CurrentGridPosition);
        }

        [Test]
        public void Not_Teleport_To_Unwalkable_Cell() {
            _grid.SetCellWalkableFlag(0,0,false);
            _gridAgent.SetGridPos(0,0);
            
            Assert.AreEqual(_grid.GetCellWorldPos(2,2), _gridAgent.WorldPosition);
            Assert.AreEqual(new Vector2Int(2,2), _gridAgent.CurrentGridPosition);
        }

        [Test]
        public void Move_Agent_To_Cell_1_1() {
            _gridAgent.MoveAgentToDirection(-Vector2Int.one);
            
            Assert.AreEqual(_grid.GetCellWorldPos(1,1), _gridAgent.WorldPosition);
            Assert.AreEqual(new Vector2Int(1, 1), _gridAgent.CurrentGridPosition);
        }
    }
}