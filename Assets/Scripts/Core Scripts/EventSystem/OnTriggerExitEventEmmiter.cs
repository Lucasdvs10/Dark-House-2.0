using UnityEngine;

namespace GameScripts.GameEvent {
    public class OnTriggerExitEventEmmiter : MonoBehaviour {
        [SerializeField] private SOBaseGameEvent _eventToEmmit;

        private void OnTriggerExit2D(Collider2D col) {
            _eventToEmmit.InvokeEvent();
        }    }
}