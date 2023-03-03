using UnityEngine;
using UnityEngine.Events;

namespace GameScripts.GameEvent {
    public class BaseGameEventListener : MonoBehaviour {
        [SerializeField] private SOBaseGameEvent _eventToListen;
        public UnityEvent UnityEventEmiter;

        private void OnEnable() {
            _eventToListen.Subscribe(UnityEventEmiter.Invoke);
        }

        private void OnDisable() {
            _eventToListen.Unsubscribe(UnityEventEmiter.Invoke);
        }
    }
}