using Core_Scripts.GridSystem;
using NUnit.Framework;
using UnityEngine;

namespace Unit_Tests {
    public class GridEntityShould {
        private GridEntity _gridEntity;
        
        [SetUp]
        public void SetUp_All_Tests() {
            _gridEntity = new GridEntity(10, 10, new Vector2(3.5f, 4f));
        }

        [Test]
        public void Return_A_10_by_10_grid() {
            Assert.AreEqual(10, _gridEntity.RowsAmount);
            Assert.AreEqual(10, _gridEntity.ColumnsAmount);
        }

        [Test]
        public void Return_World_Position_As_3_And_Half() {
            var worldPos = _gridEntity.GridEntityWorldPos;
            
            Assert.AreEqual(new Vector2(3.5f, 4f), worldPos);
        }

        [Test]
        public void Return_A_Vector_3Point5x_4y() {
            var cellWorldPos = _gridEntity.GetCellWorldPos(0,1);
            
            Assert.AreEqual(new Vector2(4.5f, 4f), cellWorldPos);
        }

        [Test]
        public void Return_WorldPos_Of_Cell_3_10() {
            var cellWorldPos = _gridEntity.GetCellWorldPos(3, 12);
            
            Assert.AreEqual(_gridEntity.GetCellWorldPos(3,10), cellWorldPos);
        }
        
        
        [Test]
        public void Return_WorldPos_Of_Cell_0_0() {
            var cellWorldPos = _gridEntity.GetCellWorldPos(-4,-4);
            
            Assert.AreEqual(_gridEntity.GetCellWorldPos(0,0), cellWorldPos);
        }

        [Test]
        public void Set_Cell_1_1_To_Unwalkable() {
            _gridEntity.SetCellWalkableFlag(1,1,false);
            
            Assert.IsFalse(_gridEntity.GetCell(1,1).walkable);
        }
    }
}