using Core_Scripts.SOSingletons;
using GameScripts.GameEvent;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core_Scripts.GridSystem.MonoBehaviours {
    public class PlayerInputReader : MonoBehaviour {
        [SerializeField] private SOBaseGameEvent _playerPressedButtonEventToEmit;
        [SerializeField] private SOBaseGameEvent _playerReleasedButtonEventToEmit;
        [SerializeField] private SOVec2IntSingleton _playerKeyPressedSingleton;
        [SerializeField] private SOBaseGameEvent _spacePressedEvent;

        public void ReadWASDInput(InputAction.CallbackContext ctx) {
            if (ctx.performed) {
                var directionRed = ctx.ReadValue<Vector2>();
                var directionInGrid = new Vector2Int((int) -directionRed.y, (int)directionRed.x);
                _playerKeyPressedSingleton.Value = directionInGrid;
                
                if(directionInGrid != Vector2Int.zero)
                    _playerPressedButtonEventToEmit.InvokeEvent();
                if(directionInGrid == Vector2Int.zero)
                    _playerReleasedButtonEventToEmit.InvokeEvent();
            }
        }

        public void ReadSpaceKeyInput(InputAction.CallbackContext ctx) {
            if (ctx.performed) {
                _spacePressedEvent.InvokeEvent();
            }
        }

    }
}