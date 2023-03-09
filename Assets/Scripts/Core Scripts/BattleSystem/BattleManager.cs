using System.Collections.Generic;
using Core_Scripts.BattleSystem.PlayerValidatorCommand;
using Core_Scripts.BattleSystem.Timer;
using Core_Scripts.SOSingletons;
using GameScripts.GameEvent;
using Unit_Tests;
using UnityEngine;

namespace Core_Scripts.BattleSystem {
    [RequireComponent(typeof(IPlayerValidatorCommand), typeof(ISoundQueueGenerator), typeof(AudioTimer))]
    public class BattleManager : MonoBehaviour {
        [SerializeField] private int _queueBattleLenght;
        [SerializeField] private SOVec2IntSingleton _playerInputSingleton;
        [SerializeField] private SOBaseGameEvent _playerInputEvent;
        [SerializeField] private SOBaseGameEvent _missedAttackEventToEmit;
        [SerializeField] private SOBaseGameEvent _correctAttackEventToEmit;
        [SerializeField] private SOBaseGameEvent _endBattleEventToEmit;
        [SerializeField] private SOBaseGameEvent _startBattleEventToEmit;
        
        private IPlayerValidatorCommand _playerValidatorCommand;
        private ISoundQueueGenerator _soundQueueGenerator;
        private Queue<string> _generatedBattleQueue;
        private AudioTimer _audioTimer;

        private void Awake() {
            _playerValidatorCommand = GetComponent<IPlayerValidatorCommand>();
            _soundQueueGenerator = GetComponent<ISoundQueueGenerator>();
            _audioTimer = GetComponent<AudioTimer>();
        }

        private void OnEnable() {
            _generatedBattleQueue = _soundQueueGenerator.GenerateSoundQueue(_queueBattleLenght);
            
            _audioTimer.StartTimer();
            _playerInputEvent.Subscribe(VerifyPlayerAttack);
            _startBattleEventToEmit.InvokeEvent();
        }

        private void OnDisable() {
            _playerInputEvent.Unsubscribe(VerifyPlayerAttack);
        }

        public void VerifyPlayerAttack() {
            if (_queueBattleLenght <= 0) {
                print("O tamanho da lista é zero!");
                _endBattleEventToEmit.InvokeEvent();
                return;
            }
            
            var playerMovement = _playerInputSingleton.Value;

            bool isAttackCorrect = _playerValidatorCommand.ValidateCommand(playerMovement, ref _generatedBattleQueue);
            bool isBattleOver = _generatedBattleQueue.Count <= 0;
            
            if(!isAttackCorrect)
                _missedAttackEventToEmit.InvokeEvent();
            
            if(isAttackCorrect)
               _correctAttackEventToEmit.InvokeEvent();
            
            if(isBattleOver)
                _endBattleEventToEmit.InvokeEvent();
            
            print($"O tamanho da fila é: {_generatedBattleQueue.Count}");
        }
    }
}