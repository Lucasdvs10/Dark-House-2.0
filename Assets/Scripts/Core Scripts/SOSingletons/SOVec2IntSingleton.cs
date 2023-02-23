using UnityEngine;

namespace Core_Scripts.SOSingletons {
    [CreateAssetMenu(fileName = "new Vect 2 Int Singleton", menuName = "Singletons/Vector 2 Int", order = 0)]
    public class SOVec2IntSingleton : ScriptableObject {
        public Vector2Int Value;

        private void OnDisable() {
            Value = Vector2Int.zero;
        }
    }
}