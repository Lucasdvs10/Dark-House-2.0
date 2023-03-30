using System.Collections.Generic;
using Core_Scripts.BattleSystem.PlayerValidatorCommand;
using NUnit.Framework;
using UnityEngine;

namespace Unit_Tests {
    public class PlayerValidadorCommandShould {
        private PlayerValidadorCommand _playerValidadorCommand;
        private Dictionary<Vector2Int, string[]> _commandMapMock;
        private Queue<string> _queueToDefeatMock;

        [SetUp]
        public void SetUpTests() {
            var songArray = new[] { "Som1" };
            var songArray2 = new[] { "Som2" };
            var songArray3 = new[] { "Som3" };
            var songArray4 = new[] { "Som4" };
            _commandMapMock = new Dictionary<Vector2Int, string[]>() {
                {Vector2Int.left, songArray},
                {Vector2Int.down, songArray2},
                {Vector2Int.up, songArray3},
                {Vector2Int.right, songArray4},
            };
            
            _playerValidadorCommand = new PlayerValidadorCommand(_commandMapMock);
        }

        [Test]
        public void Return_True_When_Input_Is_Vec2IntLeft() {
            var list = new List<string> { "Som1" };

            _queueToDefeatMock = new Queue<string>(list);
            var response = _playerValidadorCommand.Validate(Vector2Int.left, ref _queueToDefeatMock);
            
            Assert.IsTrue(response);
        }
        
        [Test]
        public void Return_True_When_Input_Is_Vec2IntRigt_And_Multiples_Sounds_On_List() {
            var list = new List<string> { "Som1" , "Som2", "Som4"};
            _queueToDefeatMock = new Queue<string>(list);
            
            var response = _playerValidadorCommand.Validate(Vector2Int.left, ref _queueToDefeatMock);
            response = _playerValidadorCommand.Validate(Vector2Int.down, ref _queueToDefeatMock);
            response = _playerValidadorCommand.Validate(Vector2Int.right, ref _queueToDefeatMock);
            
            Assert.IsTrue(response);
            Assert.IsEmpty(_queueToDefeatMock);
        }

        [Test]
        public void Not_Remove_Sound_When_Command_Is_Wrong() {
            var list = new List<string> { "Som1" , "Som2", "Som4"};
            _queueToDefeatMock = new Queue<string>(list);

            var response = _playerValidadorCommand.Validate(Vector2Int.right, ref _queueToDefeatMock);
            
            Assert.IsFalse(response);
            Assert.AreEqual(3, _queueToDefeatMock.Count);
        }
    }
}