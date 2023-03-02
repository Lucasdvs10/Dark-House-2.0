using System.Collections.Generic;
using Core_Scripts.ExtentionMethods;
using UnityEngine;

namespace Core_Scripts.BattleSystem.PlayerValidatorCommand {
    public class PlayerValidatorCommandBehaviour : MonoBehaviour, IPlayerValidatorCommand {
        private PlayerValidadorCommand _playerValidadorCommand;
        private Dictionary<Vector2Int, string> _commandSoundMap;

        [Header("Dicionario de sons")] 
        [SerializeField] private Vector2Int[] _commandKeysArray;
        [SerializeField] private AudioClip[] _audioClipsArray;

        private void Awake() {
            _commandSoundMap = new Dictionary<Vector2Int, string>();
            var soundNamesValues = _audioClipsArray.GetAllClipsNamesArray();

            for (int i = 0; i < _commandKeysArray.Length; i++) {
                _commandSoundMap.Add(_commandKeysArray[i], soundNamesValues[i]);
            }

            _playerValidadorCommand = new PlayerValidadorCommand(_commandSoundMap);
        }

        public bool ValidateCommand(Vector2Int command, ref Queue<string> commandsToDefeat) {
            if (_commandSoundMap.ContainsKey(command)) {
                return _playerValidadorCommand.Validate(command, ref commandsToDefeat);
            }
            
            return false;
        }
    }
}