using System.Collections.Generic;
using GameScripts.GameEvent;
using UnityEngine;
using Random = System.Random;

namespace Core_Scripts.KeySystem {
    public class KeyManager : MonoBehaviour {
        [SerializeField] private int _numberOfKeysToColect;
        [SerializeField] private SOBaseGameEvent _allKeysColectedEvent;
        private Key[] _allKeysArray;
        private int _keysColectedAmount = 0;
        

        private void Awake() {
            _allKeysArray = GetComponentsInChildren<Key>();
        }

        private void Start() {
            Random rn = new Random();

            HashSet<int> indexSortedSet = new HashSet<int>();

            for (int i = 0; i < _allKeysArray.Length - _numberOfKeysToColect; i++) {
                var index = rn.Next(_allKeysArray.Length);
                
                while (indexSortedSet.Contains(index)) {
                    index = rn.Next(_allKeysArray.Length);
                }

                if (!indexSortedSet.Contains(index)) {
                    indexSortedSet.Add(index);
                }
            }

            foreach (var index in indexSortedSet) {
                _allKeysArray[index].gameObject.SetActive(false);
            }
        }

        public void AddKeyColected() {
            _keysColectedAmount++;

            if (_keysColectedAmount >= _numberOfKeysToColect) {
                _allKeysColectedEvent.InvokeEvent();
            }
        }

    }
}