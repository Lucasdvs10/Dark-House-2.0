using System.Collections.Generic;
using UnityEngine;

namespace Core_Scripts.BattleSystem.PlayerValidatorCommand {
    public class PlayerValidatorCommandBehaviour : MonoBehaviour, IPlayerValidatorCommand {
        private PlayerValidadorCommand _playerValidadorCommand;
        private Dictionary<Vector2Int, string> _commandSoundMap;

        [Header("Dicionario de sons")] 
        [SerializeField] private Vector2Int[] _commandKeysArray;
        [SerializeField] private string[] _soundNamesValues;

        private void Awake() {
            _commandSoundMap = new Dictionary<Vector2Int, string>();

            for (int i = 0; i < _commandKeysArray.Length; i++) {
                _commandSoundMap.Add(_commandKeysArray[i], _soundNamesValues[i]);
            }

            _playerValidadorCommand = new PlayerValidadorCommand(_commandSoundMap);
        }

        public bool ValidateCommand(Vector2Int command, Queue<string> commandsToDefeat) {
            return _playerValidadorCommand.Validate(command, commandsToDefeat);
        }
    }
}