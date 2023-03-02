using System.Collections.Generic;
using NUnit.Framework;

namespace Unit_Tests {
    public class SoundQueueGeneratorShould {
        private SoundQueueGenerator _queueGenerator;
        private List<string> _allSoundsListMock;

        [SetUp]
        public void SetUpTests() {
            _allSoundsListMock = new List<string>() { "Som1", "Som2", "Som3", "Som4" };

            _queueGenerator = new SoundQueueGenerator(_allSoundsListMock);
        }

        [Test]
        public void Return_Queue_With_Elements_Amount_Specified() {
            var queueGenerated = _queueGenerator.GenerateSoundQueueToDefeat(4);
            Assert.AreEqual(4, queueGenerated.Count);
            
            queueGenerated = _queueGenerator.GenerateSoundQueueToDefeat(2);
            Assert.AreEqual(2, queueGenerated.Count);
            
            queueGenerated = _queueGenerator.GenerateSoundQueueToDefeat(7);
            Assert.AreEqual(7, queueGenerated.Count);
        }
    }
}