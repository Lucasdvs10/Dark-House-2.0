using System;
using UnityEngine;

namespace GameScripts.GameEvent{
    [CreateAssetMenu(fileName = "New TypedGameEvent", menuName = "Typed Game Event")]
    public class SOTypedGameEvent<T> : ScriptableObject{
        private event Action<T> OnEventRaised;
        public void InvokeEvent(T value) {
            OnEventRaised?.Invoke(value);
        }
        
        public void Subscribe(Action<T> action) {
            OnEventRaised += action;
        }

        public void Unsubscribe(Action<T> action) {
            OnEventRaised -= action;
        }
    }
}
