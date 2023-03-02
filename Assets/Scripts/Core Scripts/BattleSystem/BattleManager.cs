using System;
using System.Collections.Generic;
using Core_Scripts.BattleSystem.PlayerValidatorCommand;
using Core_Scripts.SOSingletons;
using GameScripts.GameEvent;
using Unit_Tests;
using UnityEngine;

namespace Core_Scripts.BattleSystem {
    [RequireComponent(typeof(IPlayerValidatorCommand), typeof(ISoundQueueGenerator))]
    public class BattleManager : MonoBehaviour {
        [SerializeField] private int _queueBattleLenght;
        [SerializeField] private SOVec2IntSingleton _playerInputSingleton;
        [SerializeField] private SOBaseGameEvent _playerInputEvent;
        
        private IPlayerValidatorCommand _playerValidatorCommand;
        private ISoundQueueGenerator _soundQueueGenerator;
        private Queue<string> _generatedBattleQueue;

        private void Awake() {
            _playerValidatorCommand = GetComponent<IPlayerValidatorCommand>();
            _soundQueueGenerator = GetComponent<ISoundQueueGenerator>();
        }

        private void OnEnable() {
            _playerInputEvent.Subscribe(VerifyPlayerAttack);
        }

        private void OnDisable() {
            _playerInputEvent.Unsubscribe(VerifyPlayerAttack);
        }

        private void Start() {
            _generatedBattleQueue = _soundQueueGenerator.GenerateSoundQueue(_queueBattleLenght);

            foreach (var sound in _generatedBattleQueue) {
                print(sound);
            }
        }

        public void VerifyPlayerAttack() {
            if (_queueBattleLenght <= 0) {
                print("O tamanho da lista é zero!");
                return;
            }
            
            var playerMovement = _playerInputSingleton.Value;

            bool acertou = _playerValidatorCommand.ValidateCommand(playerMovement, ref _generatedBattleQueue);
            
            print($"O tamanho da fila é: {_generatedBattleQueue.Count}");
        }
    }
}