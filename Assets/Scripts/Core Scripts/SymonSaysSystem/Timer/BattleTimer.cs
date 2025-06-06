using System.Collections;
using GameScripts.GameEvent;
using UnityEngine;
using UnityEngine.Events;

namespace Core_Scripts.BattleSystem.Timer {
    public class BattleTimer : MonoBehaviour {
        [SerializeField] private SOBaseGameEvent _endBattleEvent;
        [SerializeField] private float _battleDurationInSeconds;
        [SerializeField] private UnityEvent<int> _partTimerEvent;
        private float _currentTime;

        [ContextMenu("Iniciar Timer")]
        public void StartTimerCoroutine(int divideAudioInParts) {
            StartCoroutine(TimerCoroutine(1f, divideAudioInParts));
        }

        public void StopTimerCoroutine() {
            StopAllCoroutines();
        }
        
        public void DecreaseTime(float time) {
            _currentTime -= time;
        }

        private IEnumerator TimerCoroutine(float deltaTime, int timerDividedIn) {
            _currentTime = _battleDurationInSeconds;
            var timerPart = _battleDurationInSeconds / timerDividedIn; //Nome bosta para uma variável
            int currentIndex = 0;
            
            float nextTimeToEmitPartEvent = _currentTime - timerPart;
            while (_currentTime > 0) {
                _currentTime--;
                
                if (_currentTime <= nextTimeToEmitPartEvent && _currentTime > 0) {
                    _partTimerEvent.Invoke(currentIndex);
                    nextTimeToEmitPartEvent = _currentTime - timerPart;
                    currentIndex++;
                }
                
                yield return new WaitForSeconds(deltaTime);
            }
            _endBattleEvent.InvokeEvent();
        }
        
    }
}