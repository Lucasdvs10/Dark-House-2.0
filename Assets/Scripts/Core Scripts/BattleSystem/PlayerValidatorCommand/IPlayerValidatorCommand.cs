using System.Collections.Generic;
using UnityEngine;

namespace Core_Scripts.BattleSystem.PlayerValidatorCommand {
    public interface IPlayerValidatorCommand {
        bool ValidateCommand(Vector2Int command, Queue<string> commandsToDefeat);
    }
}