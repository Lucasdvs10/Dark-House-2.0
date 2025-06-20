using System;
using System.Collections;
using GameScripts.GameEvent;
using UnityEngine;

namespace Core_Scripts {
    public class LevelTimer : MonoBehaviour {
        [SerializeField] private int _timeLimitInSeconds = 600;
        [SerializeField] private SOBaseGameEvent _gameOverEvent;
        private int _elapsedTimeInSeconds;
        private float _startTime;
        private LevelTimerUI[] _levelTimerUI;

        private void Awake() {
            _levelTimerUI = GetComponentsInChildren<LevelTimerUI>(true);
        }

        private void Start() {
            StartTimer();
        }

        [ContextMenu("Start Timer")]
        public void StartTimer() {
            _startTime = Time.time;
            
            foreach (var timerUI in _levelTimerUI) {
                timerUI.PlaySound();
            }
            
            StartCoroutine(StartTimerCO());
        }
        
        [ContextMenu("Resume Timer")]
        public void ResumeTimer() {
            StartCoroutine(StartTimerCO());
        }
        
        [ContextMenu("Pause Timer")]
        public void PauseTimer() {
            _elapsedTimeInSeconds = Mathf.RoundToInt(Time.time - _startTime);
            StopAllCoroutines();
            
            Debug.Log($"Tempo passado: {_elapsedTimeInSeconds} segundos");
        }

        IEnumerator StartTimerCO() {
            yield return new WaitForSeconds(_timeLimitInSeconds - _elapsedTimeInSeconds);
            _gameOverEvent.InvokeEvent();
        }
    }
}