using UnityEngine;

namespace Core_Scripts.GridSystem.MonoBehaviours {
    public class RotatePlayer : MonoBehaviour {
        public void RotatePlayerGameObj(Vector2Int direction) {
            if (direction == Vector2Int.up) {
                transform.Rotate(0,0,-90);
            }
            else if (direction == Vector2Int.down) {
                transform.Rotate(0,0,90);
            }
        }
    }
}