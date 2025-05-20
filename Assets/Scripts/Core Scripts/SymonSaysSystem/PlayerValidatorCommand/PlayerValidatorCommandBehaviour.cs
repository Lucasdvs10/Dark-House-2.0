using System.Collections.Generic;
using Core_Scripts.ExtentionMethods;
using UnityEngine;

namespace Core_Scripts.BattleSystem.PlayerValidatorCommand {
    public class PlayerValidatorCommandBehaviour : MonoBehaviour, IPlayerValidatorCommand {
        private PlayerValidadorCommand _playerValidadorCommand;
        private Dictionary<Vector2Int, string[]> _commandSoundMap;

        [Header("Dicionario de sons")] 
        [SerializeField] private Vector2Int[] _commandKeysArray;
        [SerializeField] private AudioClip[] _northAudioClipsArray;
        [SerializeField] private AudioClip[] _southAudioClipsArray;
        [SerializeField] private AudioClip[] _eastAudioClipsArray;
        [SerializeField] private AudioClip[] _westAudioClipsArray;

        private void Awake() {
            _commandSoundMap = new Dictionary<Vector2Int, string[]>();
            var soundNamesValues = _northAudioClipsArray.GetAllClipsNamesArray();

            _commandSoundMap.Add(_commandKeysArray[0], _northAudioClipsArray.GetAllClipsNamesArray());
            _commandSoundMap.Add(_commandKeysArray[1], _southAudioClipsArray.GetAllClipsNamesArray());
            _commandSoundMap.Add(_commandKeysArray[2], _eastAudioClipsArray.GetAllClipsNamesArray());
            _commandSoundMap.Add(_commandKeysArray[3], _westAudioClipsArray.GetAllClipsNamesArray());
            
            _playerValidadorCommand = new PlayerValidadorCommand(_commandSoundMap);
        }

        public bool ValidateCommand(Vector2Int command, ref List<string> commandsToDefeat, int currentSoundIndex = 0) {
            if (_commandSoundMap.ContainsKey(command)) {
                return _playerValidadorCommand.Validate(command, ref commandsToDefeat, currentSoundIndex);
            }
            
            return false;
        }
    }
}