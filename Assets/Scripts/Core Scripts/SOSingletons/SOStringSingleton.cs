using UnityEngine;

namespace Core_Scripts.SOSingletons {
    [CreateAssetMenu(fileName = "new String Singleton", menuName = "Singletons/String", order = 0)]
    public class SOStringSingleton : ScriptableObject {
        public string Value;

        private void OnEnable() {
            Value = "";
        }
    }
}