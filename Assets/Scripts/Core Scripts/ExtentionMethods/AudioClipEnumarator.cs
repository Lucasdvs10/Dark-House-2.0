using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Core_Scripts.ExtentionMethods {
    public static class AudioClipEnumarator {
        public static string[] GetAllClipsNamesArray(this AudioClip[] audioClipsArray) {
            var arrayToReturn = new string[audioClipsArray.Length];

            for (int i = 0; i < audioClipsArray.Length; i++) {
                arrayToReturn[i] = audioClipsArray[i].name;
            }

            return arrayToReturn;
        }
        public static AudioClip GetRandomClipFromList(this List<AudioClip> audioClipsList) {
            Random rn = new Random();

            var randomIndex = rn.Next(audioClipsList.Count);
            return audioClipsList[randomIndex];
        }
    }
}