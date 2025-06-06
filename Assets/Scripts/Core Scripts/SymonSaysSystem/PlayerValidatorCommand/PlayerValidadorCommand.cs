using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core_Scripts.BattleSystem.PlayerValidatorCommand {
    public class PlayerValidadorCommand {
        private Dictionary<Vector2Int, string[]> _commandAudioMap = new Dictionary<Vector2Int, string[]>();

        public bool Validate(Vector2Int command, ref List<string> commandsToDefeat, int currentSoundIndex = 0) {
            var currentSound = commandsToDefeat[currentSoundIndex];
            var desiredSoundArray = _commandAudioMap[command];

            var isComandCorrect = desiredSoundArray.Contains(currentSound);

            return isComandCorrect;
        }
        
        
        public PlayerValidadorCommand(Dictionary<Vector2Int, string[]> commandAudioMap) {
            _commandAudioMap = commandAudioMap;
        }
        
        public Dictionary<Vector2Int, string[]> CommandAudioMap => _commandAudioMap;
    }
}