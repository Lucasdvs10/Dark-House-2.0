using Core_Scripts.KeySystem;
using GameScripts.GameEvent;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core_Scripts {
    public class Level3KeyManager : MonoBehaviour, IManagerCounter{
        [SerializeField] private int shuffleAmount = 1;
        [SerializeField] private SOIntGameEvent _keyCollectedEvent;
        [SerializeField] private SOBaseGameEvent _allKeyCollectedEvent;
        [SerializeField] private SOBaseGameEvent _correctKeyCollectedEvent;
        [SerializeField] private SOBaseGameEvent _wrongKeyCollectedEvent;
        private Level3Key[] _allKeysArray;
        private int _nextKeyToCollectIndex;

        private void Awake() {
            _allKeysArray = GetComponentsInChildren<Level3Key>();
            _nextKeyToCollectIndex = 0;

            for (int i = 0; i < _allKeysArray.Length; i++) {
                _allKeysArray[i].SetId(i);
            }
            
            
            ShuffleKeysPositions();
        }

        private void ShuffleKeysPositions() {
            for (int i = 0; i < shuffleAmount; i++) {
                int index1 = Random.Range(0, _allKeysArray.Length);
                int index2 = Random.Range(0, _allKeysArray.Length);
                
                (_allKeysArray[index1].transform.position, _allKeysArray[index2].transform.position) =
                    (_allKeysArray[index2].transform.position, _allKeysArray[index1].transform.position);
            }
        }

        private void OnEnable() {
            _keyCollectedEvent.Subscribe(TryToCollectKey);
        }

        private void OnDisable() {
            _keyCollectedEvent.Unsubscribe(TryToCollectKey);
        }

        public void TryToCollectKey(int idOfCollectedKey) {
            if (idOfCollectedKey != _nextKeyToCollectIndex) {
                Debug.Log($"Coletou a chave errada, parça!");
                _wrongKeyCollectedEvent.InvokeEvent();
                return;
            }
            
            Debug.Log($"Coletou a chave certa :D");
            _allKeysArray[_nextKeyToCollectIndex].gameObject.SetActive(false);
            _nextKeyToCollectIndex++;
            _correctKeyCollectedEvent.InvokeEvent();

            if (_nextKeyToCollectIndex >= _allKeysArray.Length) {
                _allKeyCollectedEvent.InvokeEvent();
            }
        }

        public int ObjectsColectedAmount => _nextKeyToCollectIndex;

    }
}