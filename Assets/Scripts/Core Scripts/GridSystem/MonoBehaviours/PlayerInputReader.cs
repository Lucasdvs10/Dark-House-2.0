using System;
using Core_Scripts.SOSingletons;
using GameScripts.GameEvent;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core_Scripts.GridSystem.MonoBehaviours {
    public class PlayerInputReader : MonoBehaviour {
        [SerializeField] private SOBaseGameEvent _playerPressedButtonEventToEmit;
        [SerializeField] private SOBaseGameEvent _playerReleasedButtonEventToEmit;
        [SerializeField] private SOBaseGameEvent _pressedAorDButtonEvent;
        [SerializeField] private SOVec2IntSingleton _playerKeyPressedSingleton;
        [SerializeField] private SOBaseGameEvent _eKeyPressedEvent;
        [SerializeField] private SOBaseGameEvent _spaceKeyPressedEvent;
        [SerializeField] private SOBaseGameEvent _enterKeyPressedEvent;
        [SerializeField] private SOBaseGameEvent _blockPauseButtonEvent;
        [SerializeField] private SOBaseGameEvent _escPressedEvent;
        [SerializeField] private SOBaseGameEvent _blockPauseButtonEvent2;
        private bool _pauseButtonIsBlocked;

        public void ReadWASDInput(InputAction.CallbackContext ctx) {
            if (ctx.performed) {
                var directionRed = ctx.ReadValue<Vector2>();
                var directionInGrid = new Vector2Int(Mathf.RoundToInt(-directionRed.y), Mathf.RoundToInt(directionRed.x));
                // Cima -1, 0
                // Baixo 1, 0
                // Esquerda 0, -1
                // Direita 0, 1
                _playerKeyPressedSingleton.Value = directionInGrid;
                
                if(directionInGrid != Vector2Int.zero)
                    _playerPressedButtonEventToEmit.InvokeEvent();

                if (_pressedAorDButtonEvent is not null &&
                    (directionRed.x != 0)) {
                    _pressedAorDButtonEvent.InvokeEvent();
                }
                
                if(directionInGrid == Vector2Int.zero)
                    _playerReleasedButtonEventToEmit.InvokeEvent();
            }
        }

        private void OnEnable() {
            _blockPauseButtonEvent2.Subscribe(TogglePauseButton);
            _blockPauseButtonEvent.Subscribe(TogglePauseButton);
        }

        private void OnDisable() {
            _blockPauseButtonEvent.Unsubscribe(TogglePauseButton);
            _blockPauseButtonEvent2.Unsubscribe(TogglePauseButton);
        }

        public void ReadEscKeyInput(InputAction.CallbackContext ctx) {
            if (ctx.performed  && !_pauseButtonIsBlocked) {
                _escPressedEvent.InvokeEvent();
            }
        }

        public void TogglePauseButton() {
            if (_pauseButtonIsBlocked)
                _pauseButtonIsBlocked = false;
            else
                _pauseButtonIsBlocked = true;

        }
        
        public void ReadEKeyInput(InputAction.CallbackContext ctx) {
            if (ctx.performed) {
                _eKeyPressedEvent.InvokeEvent();
            }
        }

        public void ReadSpaceKeyInput(InputAction.CallbackContext ctx) {
            if (ctx.performed) {
                _spaceKeyPressedEvent.InvokeEvent();
            }
        }
        
        public void ReadEnterKeyInput(InputAction.CallbackContext ctx) {
            if (ctx.performed) {
                _enterKeyPressedEvent.InvokeEvent();
            }
        }
    }
}