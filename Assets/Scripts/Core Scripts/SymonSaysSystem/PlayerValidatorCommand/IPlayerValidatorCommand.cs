using System.Collections.Generic;
using UnityEngine;

namespace Core_Scripts.BattleSystem.PlayerValidatorCommand {
    public interface IPlayerValidatorCommand {
        bool ValidateCommand(Vector2Int command, ref List<string> commandsToDefeat, int currentSoundIndex = 0);
        
        public Dictionary<Vector2Int, string[]> CommandSoundMap();
    }
}