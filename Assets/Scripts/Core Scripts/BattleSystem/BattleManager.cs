using System.Collections;
using System.Collections.Generic;
using Core_Scripts.BattleSystem.PlayerValidatorCommand;
using Core_Scripts.BattleSystem.Timer;
using Core_Scripts.SOSingletons;
using GameScripts.GameEvent;
using Unit_Tests;
using UnityEngine;

namespace Core_Scripts.BattleSystem {
    [RequireComponent(typeof(IPlayerValidatorCommand), typeof(ISoundQueueGenerator), typeof(AudioTimer))]
    public class BattleManager : MonoBehaviour {
        [SerializeField] private int _phasesAmount = 1;
        [SerializeField] private SOVec2IntSingleton _playerInputSingleton;
        [SerializeField] private SOBaseGameEvent _playerInputEvent;
        [SerializeField] private SOBaseGameEvent _missedAttackEventToEmit;
        [SerializeField] private SOBaseGameEvent _correctAttackEventToEmit;
        [SerializeField] private SOBaseGameEvent _endBattleEventToEmit;
        [SerializeField] private SOBaseGameEvent _startBattleEventToEmit;
        [SerializeField] private SOBaseGameEvent _duelStateDisabled;
        
        private IPlayerValidatorCommand _playerValidatorCommand;
        private ISoundQueueGenerator _soundListGenerator;
        private List<string> _currentAudioList;
        private AudioTimer _audioTimer;
        private AudioSource _audioSourceBattleSFX;
        private int _currentPhase = 0;
        private int _currentIndexAudio;
        private float _currentAudioDuration;
        
        private void Awake() {
            _playerValidatorCommand = GetComponent<IPlayerValidatorCommand>();
            _soundListGenerator = GetComponent<ISoundQueueGenerator>();
            // _audioTimer = GetComponent<AudioTimer>();
            _audioSourceBattleSFX = GetComponent<AudioSource>();
        }

        private void OnEnable() {
            StartBattle();
        }

        private void OnDisable() {
            _duelStateDisabled.InvokeEvent();
            _playerInputEvent.Unsubscribe(VerifyPlayerAttack);
        }

        public void StartBattle() {
            _playerInputEvent.Subscribe(VerifyPlayerAttack);
            StartNextPhase();
            _startBattleEventToEmit.InvokeEvent();
            print("Iniciando Simon Says");
        }

        private void StartNextPhase() {
            _currentPhase++;
            _currentIndexAudio = 0;
            _currentAudioList = _soundListGenerator.GenerateSoundList(_currentPhase);
            StartCoroutine(PlayAllAudiosFromList());
        }

        public void StopCOsAndPlayAllAudios() {
            StopAllCoroutines();
            StartCoroutine(PlayAllAudiosFromList());
        }
        
        IEnumerator PlayAllAudiosFromList() {
            _playerInputEvent.Unsubscribe(VerifyPlayerAttack);
            
            foreach (var audio in _currentAudioList) {
                SetSoundToAudioClip(audio, 0);
                yield return new WaitForSeconds(_currentAudioDuration);
            }
            _playerInputEvent.Subscribe(VerifyPlayerAttack);
        }

        public void VerifyPlayerAttack() {
            var playerMovement = _playerInputSingleton.Value;
            bool isAttackCorrect = _playerValidatorCommand.ValidateCommand(playerMovement, ref _currentAudioList, _currentIndexAudio);
            
            if (!isAttackCorrect) {
                _missedAttackEventToEmit.InvokeEvent();
                _currentIndexAudio = 0;
                return;
            }
            
            _correctAttackEventToEmit.InvokeEvent();
            _currentIndexAudio++;
            
            if (_currentIndexAudio >= _currentAudioList.Count) {
                if (_currentPhase >= _phasesAmount) { 
                    print("Fim do jogo");
                    _endBattleEventToEmit.InvokeEvent();
                    return;
                }
                print("Fim da fase");
                StartNextPhase();
            }
        }

        private void SetSoundToAudioClip(string clipName, ulong delay) {
            var audioClip = Resources.Load<AudioClip>($"SoundSystem/SoundBattles/{clipName}");
            
            _audioSourceBattleSFX.clip = audioClip;
            _currentAudioDuration = audioClip.length;
            _audioSourceBattleSFX.PlayDelayed(delay);
        }
    }
}