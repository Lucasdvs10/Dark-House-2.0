using GameScripts.GameEvent;
using UnityEngine;

namespace Core_Scripts.GhostIASystem {
    public class OnStateEnter : MonoBehaviour {
        [SerializeField] private SOBaseGameEvent _onStateEnterGameEvent;

        private void OnEnable() {
            _onStateEnterGameEvent.InvokeEvent();
        }
    }
}