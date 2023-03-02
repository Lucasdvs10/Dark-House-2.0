using System.Collections.Generic;
using UnityEngine;

namespace Core_Scripts.BattleSystem.PlayerValidatorCommand {
    public class PlayerValidadorCommand {
        private Dictionary<Vector2Int, string> _commandAudioMap = new Dictionary<Vector2Int, string>();

        public bool Validate(Vector2Int command, ref Queue<string> commandsToDefeat) {
            var currentSound = commandsToDefeat.Peek();
            var desiredSound = _commandAudioMap[command];

            var isComandCorrect = currentSound == desiredSound;

            if (isComandCorrect) 
                commandsToDefeat.Dequeue();
            

            return isComandCorrect;
        }
        
        
        public PlayerValidadorCommand(Dictionary<Vector2Int, string> commandAudioMap) {
            _commandAudioMap = commandAudioMap;
        }
        
        public Dictionary<Vector2Int, string> CommandAudioMap => _commandAudioMap;
    }
}