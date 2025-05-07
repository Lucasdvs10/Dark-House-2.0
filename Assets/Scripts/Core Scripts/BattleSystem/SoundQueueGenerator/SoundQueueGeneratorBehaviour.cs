using System.Collections.Generic;
using System.Linq;
using Core_Scripts.ExtentionMethods;
using Unit_Tests;
using UnityEngine;

namespace Core_Scripts.BattleSystem.SoundQueueGenerator {
    public class SoundQueueGeneratorBehaviour : MonoBehaviour, ISoundQueueGenerator {
        private SoundListGenerator _soundListGenerator;

        private void Awake() {
            var allSounds = Resources.LoadAll<AudioClip>($"SoundSystem/SoundBattles");
            var allSoundNamesArray = allSounds.GetAllClipsNamesArray();

            _soundListGenerator = new SoundListGenerator(allSoundNamesArray.ToList());
        }

        public List<string> GenerateSoundList(int listLength) {
            if(_soundListGenerator is null)
                Debug.LogError("Sound Queue Generator is not initialized");
            
            return _soundListGenerator.GenerateSoundQueueToDefeat(listLength);
        }
    }
}