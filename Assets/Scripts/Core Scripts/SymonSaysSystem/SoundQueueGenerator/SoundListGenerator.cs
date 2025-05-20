using System;
using System.Collections.Generic;

namespace Core_Scripts.BattleSystem.SoundQueueGenerator {
    public class SoundListGenerator {
        private List<string> _allSoundsList;

        public List<string> GenerateSoundQueueToDefeat(int queueLength) {
            var listToReturn = new List<string>();

            for (int i = 0; i < queueLength; i++) {
                var randomSound = GetRandomItemFromList<string>(_allSoundsList);
                listToReturn.Add(randomSound);
            }
            return listToReturn;
        }

        private T GetRandomItemFromList<T>(List<T> listToPick) {
            var random = new Random();
            var randomIndex = random.Next(listToPick.Count);

            return listToPick[randomIndex];
        }
        public SoundListGenerator(List<string> allSoundsList) {
            _allSoundsList = allSoundsList;
        }
    }
}