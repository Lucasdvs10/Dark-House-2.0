using System;
using GameScripts.GameEvent;
using UnityEngine;

namespace Core_Scripts {
    public class EventTester : MonoBehaviour {
        public SOBaseGameEvent _event;

        private void OnEnable() {
            _event.Subscribe(Teste);
        }

        private void OnDisable() {
            _event.Unsubscribe(Teste);
        }

        public void Teste() {
            Debug.Log("Evento Chamado!", this);
        }
    }
}