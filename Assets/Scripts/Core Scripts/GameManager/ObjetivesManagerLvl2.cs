using System;
using Core_Scripts.KeySystem;
using GameScripts.GameEvent;
using UnityEngine;

namespace Core_Scripts {
    public class ObjetivesManagerLvl2 : MonoBehaviour, IManagerCounter{
        [SerializeField] private SOBaseGameEvent _allObjectivesCompletedEvent;
        private int _objectivesCompletedAmount = 0;
        private int _totalObjectives;

        private void Awake() {
            _totalObjectives = transform.childCount;
        }

        public void AddCompletedObjectives() {
            _objectivesCompletedAmount++;

            if (_objectivesCompletedAmount >= _totalObjectives) {
                _allObjectivesCompletedEvent.InvokeEvent();
            }
        }

        public int ObjectsColectedAmount => _objectivesCompletedAmount;
    }
}