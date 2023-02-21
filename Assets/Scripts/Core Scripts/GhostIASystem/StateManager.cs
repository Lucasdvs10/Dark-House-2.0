using UnityEngine;

namespace Core_Scripts.GhostIASystem {
    public class StateManager : MonoBehaviour, IStateManager {
        public Transform InitialState;
        
        private Transform[] _allStatesArray;

        private void Awake() {
            _allStatesArray = GetComponentsInChildren<Transform>();
            foreach (var childTransform in _allStatesArray) {
                if (childTransform != transform && childTransform != InitialState) {
                    childTransform.gameObject.SetActive(false);
                }
            }
        }

        public void ChangeState(Transform nextState) {
            foreach (var state in _allStatesArray) {
                if(state != nextState && state != transform) 
                    state.gameObject.SetActive(false);
                else
                    state.gameObject.SetActive(true);
            }
        }
    }
}