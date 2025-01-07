using System.Collections;
using UnityEngine;

namespace Core_Scripts.GhostIASystem {
    public class MoveTowardsObject : MonoBehaviour {
        [SerializeField] private Transform[] _targetTransformsArray;
        [SerializeField] private float _radiousToDefineArrival;
        [SerializeField] private Transform transformToMove;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float _buffDurationInSeconds;
        private int _currentTargetIndex = 0;
        
        private void Update() {
            transformToMove.position = Vector2.MoveTowards(transformToMove.position, _targetTransformsArray[_currentTargetIndex]
            .position, Time.deltaTime * movementSpeed);
            var targetIsWithinRange =
                Vector2.Distance(transformToMove.position, _targetTransformsArray[_currentTargetIndex].position) <=
                _radiousToDefineArrival;
            
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
        
        public void SetMovementSpeed(float newSpeed) {
            movementSpeed = newSpeed;
        }
        
        public void StartBuffMovementSpeedTemporaraly(float newSpeed) {
            StartCoroutine(BuffMovementSpeedTemporaralyCO(newSpeed, _buffDurationInSeconds));
        }
        
        private IEnumerator BuffMovementSpeedTemporaralyCO(float newSpeed, float durationInSeconds) {
            float originalSpeed = movementSpeed;
            SetMovementSpeed(newSpeed);
            
            yield return new WaitForSeconds(durationInSeconds);
            
            SetMovementSpeed(originalSpeed);
        }
    }
}