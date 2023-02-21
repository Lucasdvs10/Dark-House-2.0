using GameScripts.GameEvent;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Core_Scripts.GhostIASystem {
    public class MovingDetectorTrigger : MonoBehaviour {
        public UnityEvent<Transform> PlayerDetectedEventToEmmit;
        public Transform NextState;
        
        [SerializeField] private float perceivePlayerRadious;
        [SerializeField] private SOBaseGameEvent playerMovedEventToListen;

        private Transform _playersTransform;
        private IStateManager _stateManager;
        
        private void Awake() {
            _playersTransform = GameObject.FindWithTag("Player").transform;
            _stateManager = GetComponentInParent<IStateManager>();
        }

        private void OnEnable() {
            playerMovedEventToListen.Subscribe(PlayerMovedEventRaised);
            PlayerDetectedEventToEmmit.AddListener(_stateManager.ChangeState);
        }

        private void OnDisable() {
            playerMovedEventToListen.Unsubscribe(PlayerMovedEventRaised);
            PlayerDetectedEventToEmmit.RemoveListener(_stateManager.ChangeState);
        }

        private void PlayerMovedEventRaised() {
            if (Vector2.Distance(_playersTransform.position, transform.position) <= perceivePlayerRadious) {
                PlayerDetectedEventToEmmit.Invoke(NextState);
            }
        }

        private void OnDrawGizmos() {
            Gizmos.DrawWireSphere(transform.position, perceivePlayerRadious);
        }
    }
}