using UnityEngine;

namespace Core_Scripts.GhostIASystem {
    public class MoveTowardsObject : MonoBehaviour {
        [SerializeField] private Transform[] _targetTransformsArray;
        [SerializeField] private float _perceiveTargetRadious;
        [SerializeField] private Transform transformToMove;
        [SerializeField] private float movementSpeed;
        private int _currentTargetIndex = 0;
        
        private void Update() {
            transformToMove.position = Vector2.MoveTowards(transformToMove.position, _targetTransformsArray[_currentTargetIndex]
            .position, Time.deltaTime * movementSpeed);
            var targetIsWithinRange =
                Vector2.Distance(transformToMove.position, _targetTransformsArray[_currentTargetIndex].position) <=
                _perceiveTargetRadious;
            
            if (targetIsWithinRange) {
                GetNextIndex();
            }
        }

        private void GetNextIndex() {
            if (_currentTargetIndex >= _targetTransformsArray.Length - 1) {
                _currentTargetIndex = 0;
                return;
            }
            _currentTargetIndex++;
        }
        
    }
}