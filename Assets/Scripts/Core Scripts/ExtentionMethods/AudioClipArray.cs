using UnityEngine;

namespace Core_Scripts.ExtentionMethods {
    public static class AudioClipArray {
        public static string[] GetAllClipsNamesArray(this AudioClip[] audioClipsArray) {
            var arrayToReturn = new string[audioClipsArray.Length];

            for (int i = 0; i < audioClipsArray.Length; i++) {
                arrayToReturn[i] = audioClipsArray[i].name;
            }

            return arrayToReturn;
        }
    }
}