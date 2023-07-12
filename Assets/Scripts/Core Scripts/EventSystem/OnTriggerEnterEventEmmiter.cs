using UnityEngine;

namespace GameScripts.GameEvent {
    public class OnTriggerEnterEventEmmiter : MonoBehaviour {
        [SerializeField] private SOBaseGameEvent _eventToEmmit;

        private void OnTriggerEnter2D(Collider2D col) {
            _eventToEmmit.InvokeEvent();
        }
    }
}