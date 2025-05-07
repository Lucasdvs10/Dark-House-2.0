using System.Collections.Generic;
using Core_Scripts.BattleSystem.SoundQueueGenerator;
using NUnit.Framework;

namespace Unit_Tests {
    public class SoundQueueGeneratorShould {
        private SoundListGenerator _listGenerator;
        private List<string> _allSoundsListMock;

        [SetUp]
        public void SetUpTests() {
            _allSoundsListMock = new List<string>() { "Som1", "Som2", "Som3", "Som4" };

            _listGenerator = new SoundListGenerator(_allSoundsListMock);
        }

        [Test]
        public void Return_Queue_With_Elements_Amount_Specified() {
            var queueGenerated = _listGenerator.GenerateSoundQueueToDefeat(4);
            Assert.AreEqual(4, queueGenerated.Count);
            
            queueGenerated = _listGenerator.GenerateSoundQueueToDefeat(2);
            Assert.AreEqual(2, queueGenerated.Count);
            
            queueGenerated = _listGenerator.GenerateSoundQueueToDefeat(7);
            Assert.AreEqual(7, queueGenerated.Count);
        }
    }
}