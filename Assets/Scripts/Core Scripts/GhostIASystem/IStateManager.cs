using UnityEngine;

namespace Core_Scripts.GhostIASystem {
    public interface IStateManager {
        void ChangeState(Transform nextState);
    }
}