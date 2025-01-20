using System.Collections;
using UnityEngine;

namespace Core_Scripts.GridSystem.MonoBehaviours {
    public class RotatePlayer : MonoBehaviour {
        [SerializeField] private float _rotationTimeInSecs = 0.4f;
        
        public IEnumerator RotatePlayerGameobjCO(Vector2Int direction) {
            var currentAngle = transform.rotation.eulerAngles.z;
            var targetRotation = Quaternion.Euler(0,0, -90 * direction.y + currentAngle);
            while (transform.rotation != targetRotation) {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, (90/_rotationTimeInSecs) * Time.deltaTime);
                yield return null;
            }
        }
    }
}