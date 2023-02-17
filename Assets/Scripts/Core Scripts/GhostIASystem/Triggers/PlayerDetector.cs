using UnityEngine;
using UnityEngine.Events;

namespace Core_Scripts.GhostIASystem {
    public class PlayerDetector : MonoBehaviour {
        private Transform _playersTransform;
        public UnityEvent<Transform> PlayerDetectedEvent;
        public Transform NextState;
        [SerializeField] private float perceivePlayerRadious;

        private void Awake() {
            _playersTransform = GameObject.FindWithTag("Player").transform;
        }

        private void Update() {
            if (Vector2.Distance(_playersTransform.position, transform.position) <= perceivePlayerRadious) {
                PlayerDetectedEvent.Invoke(NextState);
            }
        }

        private void OnDrawGizmos() {
            Gizmos.DrawWireSphere(transform.position, perceivePlayerRadious);
        }
    }
}