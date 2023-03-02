using System;
using System.Collections.Generic;

namespace Unit_Tests {
    public class SoundQueueGenerator {
        private List<string> _allSoundsList;

        public Queue<string> GenerateSoundQueueToDefeat(int queueLength) {
            var queueToReturn = new Queue<string>();

            for (int i = 0; i < queueLength; i++) {
                var randomSound = GetRandomItemFromList<string>(_allSoundsList);
                queueToReturn.Enqueue(randomSound);
            }
            return queueToReturn;
        }

        private T GetRandomItemFromList<T>(List<T> listToPick) {
            var random = new Random();
            var randomIndex = random.Next(listToPick.Count);

            return listToPick[randomIndex];
        }
        public SoundQueueGenerator(List<string> allSoundsList) {
            _allSoundsList = allSoundsList;
        }
    }
}