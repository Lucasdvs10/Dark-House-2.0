using UnityEngine;
using UnityEngine.Events;

namespace Core_Scripts.GhostIASystem {
    public class BaseTrigger : MonoBehaviour {
        public UnityEvent<Transform> TriggerEventToEmmit;
        public Transform NextState;

        protected IStateManager _stateManager;

        protected void Awake() {
            _stateManager = GetComponentInParent<IStateManager>();
        }

        protected void OnEnable() {
            TriggerEventToEmmit.AddListener(_stateManager.ChangeState);
        }

        protected void OnDisable() {
            TriggerEventToEmmit.RemoveListener(_stateManager.ChangeState);
        }

        protected void EmmitEventToChangeState() {
            TriggerEventToEmmit.Invoke(NextState);
        }
    }
}