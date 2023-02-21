using UnityEngine;

namespace Core_Scripts.GhostIASystem {
    public class DetectPlayerNearbyTrigger : BaseTrigger {
        [SerializeField] private float distanceToPerceivePlayer;
        private Transform _playerTransform;

        protected void Awake() {
            base.Awake();
            _playerTransform = GameObject.FindWithTag("Player").transform;
        }

        private void Update() {
            if (Vector2.Distance(transform.position, _playerTransform.position) <= distanceToPerceivePlayer) {
                EmmitEventToChangeState();
            }
        }
    }
}