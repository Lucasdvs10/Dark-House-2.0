using System;
using UnityEngine;

namespace GameScripts.GameEvent{
    [CreateAssetMenu(fileName = "IntGameEvent", menuName = "IntGameEvent")]
    public class SOIntGameEvent : ScriptableObject{
        private event Action<int> OnEventRaised;

        private void OnEnable() {
            OnEventRaised = null;
        }

        public void InvokeEvent(int value) {
            OnEventRaised?.Invoke(value);
        }
        
        public void Subscribe(Action<int> action) {
            OnEventRaised += action;
        }

        public void Unsubscribe(Action<int> action) {
            OnEventRaised -= action;
        }
    }
}