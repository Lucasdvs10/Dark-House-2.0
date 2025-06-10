using System.Collections.Generic;
using UnityEngine;

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

            var randomIndex = Random.Range(0, audioClipsList.Count);
            return audioClipsList[randomIndex];
        }
    }
}