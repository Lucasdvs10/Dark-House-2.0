using GameScripts.GameEvent;
using UnityEngine;

namespace Core_Scripts.GhostIASystem.Triggers {
    public class MovingDetectorTrigger : BaseTrigger {
        
        [SerializeField] private float perceivePlayerRadious;
        [SerializeField] private SOBaseGameEvent playerMovedEventToListen;

        private Transform _playersTransform;
        
        private void Awake() {
            base.Awake();
            _playersTransform = GameObject.FindWithTag("Player").transform;
        }

        private void OnEnable() {
            base.OnEnable();
            playerMovedEventToListen.Subscribe(PlayerMovedEventRaised);
        }

        private void OnDisable() {
            base.OnDisable();
            playerMovedEventToListen.Unsubscribe(PlayerMovedEventRaised);
        }

        private void PlayerMovedEventRaised() {
            if (Vector2.Distance(_playersTransform.position, transform.position) <= perceivePlayerRadious) {
                EmmitEventToChangeState();
            }
        }

        private void OnDrawGizmos() {
            Gizmos.DrawWireSphere(transform.position, perceivePlayerRadious);
        }
    }
}