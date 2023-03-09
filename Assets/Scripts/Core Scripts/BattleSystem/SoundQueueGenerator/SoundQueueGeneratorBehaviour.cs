using System.Collections.Generic;
using System.Linq;
using Core_Scripts.ExtentionMethods;
using UnityEngine;

namespace Unit_Tests {
    public class SoundQueueGeneratorBehaviour : MonoBehaviour, ISoundQueueGenerator {
        private SoundQueueGenerator _soundQueueGenerator;

        private void Awake() {
            var allSounds = Resources.LoadAll<AudioClip>("SoundSystem/SoundBattles");
            var allSoundNamesArray = allSounds.GetAllClipsNamesArray();

            _soundQueueGenerator = new SoundQueueGenerator(allSoundNamesArray.ToList());
        }

        public Queue<string> GenerateSoundQueue(int queueLength) {
            return _soundQueueGenerator.GenerateSoundQueueToDefeat(queueLength);
        }
    }
}