using System.Collections;
using System.Collections.Generic;
using Core_Scripts.BattleSystem.PlayerValidatorCommand;
using Core_Scripts.SOSingletons;
using GameScripts.GameEvent;
using Unit_Tests;
using UnityEngine;

namespace Core_Scripts.SymonSaysSystem {
    [RequireComponent(typeof(IPlayerValidatorCommand), typeof(ISoundQueueGenerator))]
    public class SimonSays : MonoBehaviour {
        [SerializeField] private int _phasesAmount = 1;
        [SerializeField] private float _deleayToLoopInSeconds = 5;
        [SerializeField] private SOVec2IntSingleton _playerInputSingleton;
        [SerializeField] private SOBaseGameEvent _playerInputEvent;
        [SerializeField] private SOBaseGameEvent _missedAttackEventToEmit;
        [SerializeField] private SOBaseGameEvent _correctAttackEventToEmit;
        [SerializeField] private SOBaseGameEvent _phaseClearedEventToEmit;
        [SerializeField] private SOBaseGameEvent _endBattleEventToEmit;
        [SerializeField] private SOBaseGameEvent _startBattleEventToEmit;
        [SerializeField] private SOBaseGameEvent _duelStateDisabled;
        
        private IPlayerValidatorCommand _playerValidatorCommand;
        private ISoundQueueGenerator _soundListGenerator;
        private List<string> _currentAudioList;
        private AudioSource _audioSourceBattleSFX;
        private int _currentPhase = 0;
        private int _currentIndexAudio;
        private float _currentAudioDuration;

        private Coroutine _playAllAudiosInLoopCO;
        
        private void Awake() {
            _playerValidatorCommand = GetComponentInParent<IPlayerValidatorCommand>();
            _soundListGenerator = GetComponentInParent<ISoundQueueGenerator>();
            _audioSourceBattleSFX = GetComponentInParent<AudioSource>();
        }

        private void OnDisable() {
            _duelStateDisabled.InvokeEvent();
            _playerInputEvent.Unsubscribe(VerifyPlayerAttack);
        }

        public void StartBattle() {
            if(_soundListGenerator == null)
                return;
            _playerInputEvent.Subscribe(VerifyPlayerAttack);
            StartNextPhase();
            _startBattleEventToEmit.InvokeEvent();
            print("Iniciando Simon Says");
            
        }

        private void StartNextPhase() {
            _currentPhase++;
            _currentIndexAudio = 0;
            _currentAudioList = _soundListGenerator.GenerateSoundList(_currentPhase);
            StartCoroutine(StartNextPhaseCO());
        }

        public void StartAllAudiosLoopCO() {
            _playAllAudiosInLoopCO = StartCoroutine(PlayAllAudiosInLoopCO());
        }

        IEnumerator PlayAllAudiosInLoopCO() {
            for (var i = 0; i < 10000; i++) {
                yield return new WaitForSeconds(_deleayToLoopInSeconds);
                yield return PlayAllAudiosFromList(0);
            }
        }

        public void StopCOsAndPlayAllAudios() {
            StopAllCoroutines();
            StartCoroutine(PlayAllAudiosFromList());
        }

        public IEnumerator StartNextPhaseCO() {
            yield return PlayAllAudiosFromList();
            StartAllAudiosLoopCO();
        }
        
        IEnumerator PlayAllAudiosFromList(float initialDelay = 1.5f) {
            _playerInputEvent.Unsubscribe(VerifyPlayerAttack);
            
            yield return new WaitForSeconds(initialDelay);
            foreach (var audio in _currentAudioList) {
                SetSoundToAudioClip(audio, 0);
                yield return new WaitForSeconds(_currentAudioDuration);
            }
            _playerInputEvent.Subscribe(VerifyPlayerAttack);
        }

        public void VerifyPlayerAttack() {
            var playerMovement = _playerInputSingleton.Value;
            bool isAttackCorrect = _playerValidatorCommand.ValidateCommand(playerMovement, ref _currentAudioList, _currentIndexAudio);
            
            StopCoroutine(_playAllAudiosInLoopCO);
            
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
                _phaseClearedEventToEmit.InvokeEvent();
                StartNextPhase();
            }
            else {
                var clipName = _playerValidatorCommand.CommandSoundMap()[playerMovement];
                SetSoundToAudioClip(clipName[0], 0);
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