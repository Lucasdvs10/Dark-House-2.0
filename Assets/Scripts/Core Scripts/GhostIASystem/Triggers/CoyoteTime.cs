using System;
using System.Collections;
using GameScripts.GameEvent;
using UnityEngine;

namespace Core_Scripts.GhostIASystem.Triggers {
    public class CoyoteTime : MonoBehaviour {
        [SerializeField] private int _coyoteDurationInSeconds;
        [SerializeField] private SOBaseGameEvent[] _eventsToActivateCoyoteTime;
        private MovingDetectorTrigger _triggerToManipulate;

        private void Awake() { //Do jeito que fiz, funciona especificamente com o Moving Detector Trigger, mas é possível usar inversão de dependencia para funcionar com qualquer trigger
            _triggerToManipulate = GetComponent<MovingDetectorTrigger>();
        }

        private void OnEnable() {
            foreach (var gameEvent in _eventsToActivateCoyoteTime) {
                gameEvent.Subscribe(StartDeactivateTriggerTemporaryCo);
            }
        }
        
        private void OnDisable() {
            foreach (var gameEvent in _eventsToActivateCoyoteTime) {
                gameEvent.Unsubscribe(StartDeactivateTriggerTemporaryCo);
            }
        }

        public void StartDeactivateTriggerTemporaryCo() {
            StartCoroutine(DeactivateTriggerTemporary(_coyoteDurationInSeconds));
        }
        
        private IEnumerator DeactivateTriggerTemporary(int durationInSeconds) {
            _triggerToManipulate.enabled = false;

            yield return new WaitForSeconds(durationInSeconds);

            _triggerToManipulate.enabled = true;
        }
    }
}