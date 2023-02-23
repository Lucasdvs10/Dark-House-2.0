using UnityEngine;

namespace Core_Scripts.SOSingletons {
    [CreateAssetMenu(fileName = "new Vector3 Singleton", menuName = "Singletons/Vector 3", order = 0)]
    public class SOVec2Singleton : ScriptableObject {
        public Vector3 Value;

        private void OnEnable() {
            Value = Vector3.zero;
        }
    }
}