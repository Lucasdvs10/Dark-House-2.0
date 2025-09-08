using System;
using GameScripts.GameEvent;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core_Scripts {
    public class ActivatedTrapsCounter : MonoBehaviour {
        [SerializeField] private int _trapsAmountToGameOver = 3;
        [SerializeField] SOBaseGameEvent _OnTrapActivated;
        [SerializeField] SOBaseGameEvent _gameOverEvent;
        private int _activatedTraps;

        private void OnEnable() {
            _OnTrapActivated.Subscribe(AddOneToCounter);
        }

        private void OnDisable() {
            _OnTrapActivated.Unsubscribe(AddOneToCounter);
        }

        public void AddOneToCounter() {
            _activatedTraps++;

            if (_activatedTraps >= _trapsAmountToGameOver) {
                _gameOverEvent.InvokeEvent();
            }
        }
    }
}