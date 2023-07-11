using System;
using GameScripts.GameEvent;
using UnityEngine;

namespace Core_Scripts.Tutorial {
    [RequireComponent(typeof(AudioSource), typeof(AudioSource))]
    public class TutorialAudioManager : MonoBehaviour {
        [SerializeField] private AudioClip[] _mainAudiosArray;
        [SerializeField] private AudioClip[] _guardAudiosArray;
        [SerializeField] private SOBaseGameEvent[] _triggerAudioEventsArray;
        [SerializeField] private SOBaseGameEvent[] _stopGuardAudioEventsArray;
        private AudioSource _mainAudioSource;
        private AudioSource _guardAudioSource;

        private Action[] _playAudioActionsArray;

        private void Awake() {
            var audiosSources = GetComponents<AudioSource>();

            _mainAudioSource = audiosSources[0];
            _guardAudioSource = audiosSources[1];
            _guardAudioSource.loop = true;

            _playAudioActionsArray = new Action[_mainAudiosArray.Length];
        }

        private void OnEnable() {
            for (int i = 0; i < _mainAudiosArray.Length; i++) {
                var audioIndex = i; //Preciso fazer isso pois quando eu passo essa variável para o Action, ela mantem a refenrencia
                                        //Se eu passar o i direto, na hora de invocar o evento, ele vai pegar o último valor de i, que é 2
                                        
                _playAudioActionsArray[i] = () => {
                    PlayMainAndGuardAudios(audioIndex);
                    UnsubscribeAudioFromEvent(audioIndex);
                };
                    
                _triggerAudioEventsArray[i].Subscribe(_playAudioActionsArray[i]);
            }

            for (int i = 0; i < _guardAudiosArray.Length; i++) {
                _stopGuardAudioEventsArray[i].Subscribe(StopGuardAudio);
            }
        }
        
        private void OnDisable() {
            for (int i = 0; i < _mainAudiosArray.Length; i++) {
                _triggerAudioEventsArray[i].Unsubscribe(_playAudioActionsArray[i]);
            }
            for (int i = 0; i < _guardAudiosArray.Length; i++) {
                _stopGuardAudioEventsArray[i].Unsubscribe(StopGuardAudio);
            }
        }

        public void PlayMainAndGuardAudios(int audioIndex) {
            var mainAudio = _mainAudiosArray[audioIndex];

            _mainAudioSource.clip = mainAudio;
            _mainAudioSource.Play();

            if (audioIndex >= _guardAudiosArray.Length) return;
            
            var guardAudio = _guardAudiosArray[audioIndex];
            _guardAudioSource.clip = guardAudio;
            _guardAudioSource.PlayDelayed(mainAudio.length + 0.5f);
        }

        private void UnsubscribeAudioFromEvent(int audioIndex) {
            _triggerAudioEventsArray[audioIndex].Unsubscribe(_playAudioActionsArray[audioIndex]);
        }

        private void StopGuardAudio() {
            _guardAudioSource.Stop();
        }
        
    }
}
