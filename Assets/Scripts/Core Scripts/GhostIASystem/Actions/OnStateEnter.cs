using GameScripts.GameEvent;
using UnityEngine;
using UnityEngine.Events;

namespace Core_Scripts.GhostIASystem {
    public class OnStateEnter : MonoBehaviour {
        [SerializeField] SOBaseGameEvent _soStartGameEvent;
        [SerializeField] private UnityEvent _onStateEnterGameEvent;

        private void OnEnable() {
            _onStateEnterGameEvent.Invoke();
            _soStartGameEvent.InvokeEvent();
        }
    }
}