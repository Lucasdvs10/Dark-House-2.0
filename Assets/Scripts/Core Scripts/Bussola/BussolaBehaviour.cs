using Core_Scripts.GridSystem.MonoBehaviours;
using UnityEngine;

namespace Core_Scripts.Bussola {
    public class BussolaBehaviour : MonoBehaviour {
        [SerializeField] private AudioSource _northAudioSource;
        [SerializeField] private AudioSource _southAudioSource;
        [SerializeField] private AudioSource _eastAudioSource;
        [SerializeField] private AudioSource _westAudioSource;
        private PlayerGridAgentBehaviour _playerGridAgent;
        private bool _compassIsOn;

        private void Awake() {
            _playerGridAgent = GetComponentInParent<PlayerGridAgentBehaviour>();
            _compassIsOn = false;
        }

        private void Start() {
            transform.parent = null;
        }

        public void TurnOnCompass() {
            if (!_compassIsOn) {
                _compassIsOn = true;
            }
            else
                _compassIsOn = false;
            
            PlayCompassSoundBasedOnDirectionIfIsOn();
        }
        
        public void PlayCompassSoundBasedOnDirectionIfIsOn() {
            if (_compassIsOn) {
                PlayCompassSound(_playerGridAgent.HeadDirections[_playerGridAgent.CurrentDirection]);
            }
            else {
                StopAllAudioSources();
            }
        }

        public void PlayCompassSound(Vector2Int direction) {
            StopAllAudioSources();

            if(direction == Vector2Int.left)
                _northAudioSource.Play();
            else if(direction == Vector2Int.right)
                _southAudioSource.Play();
            else if(direction == Vector2Int.up)
                _eastAudioSource.Play();
            else
                _westAudioSource.Play();
        }

        public void StopAllAudioSources() {
            _northAudioSource.Stop();
            _southAudioSource.Stop();
            _eastAudioSource.Stop();
            _westAudioSource.Stop();
        }
       
        
    }
}