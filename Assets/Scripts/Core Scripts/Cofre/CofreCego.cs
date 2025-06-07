using Core_Scripts.SOSingletons;
using GameScripts.GameEvent;
using UnityEngine;

public class CofreCego : MonoBehaviour
{
    [SerializeField] private SOVec2IntSingleton _playerInputSingleton; 
    [SerializeField] private SOBaseGameEvent _playerInputEvent;
    [SerializeField] private SOBaseGameEvent _confirmationBtnPressedEvent;
    [SerializeField] private SOBaseGameEvent _miniGameStarted;
    [SerializeField] private SOBaseGameEvent _miniGameEnded;
    [Header("Senha do Cofre (0 a 20)")]
    [Range(0, 20)] public int codeDigit1;
    [Range(0, 20)] public int codeDigit2;
    [Range(0, 20)] public int codeDigit3;

    [Header("Áudios")]
    public AudioClip dialTurnClip;
    public AudioClip failClip;
    public AudioClip successClip;
    public AudioClip correctDigitClip;
    private AudioSource audioSource;


    [Header("Ordem de Giro")]
    [Tooltip("Se verdadeiro: Esquerda-Direita-Esquerda | Se falso: Direita-Esquerda-Direita")]
    public bool isLeftRightLeft = true;

    private int[] codeSequence;
    private bool[] directionPattern;

    private int currentInputIndex = 0;
    private int currentDialPosition = 0;
    private enum Direction { None, Left, Right }
    private Direction lastTurnDirection = Direction.None;

    private void OnEnable() {
        _playerInputEvent.Subscribe(HandleDialInput);
        _confirmationBtnPressedEvent.Subscribe(HandleConfirmation);
        _miniGameStarted.InvokeEvent();
    }

    private void OnDisable() {
        _playerInputEvent.Unsubscribe(HandleDialInput);
        _confirmationBtnPressedEvent.Unsubscribe(HandleConfirmation);
    }

    void Awake()
    {
        Application.targetFrameRate = 60; // Limita FPS a 60

        audioSource = GetComponent<AudioSource>();

        codeSequence = new int[] { codeDigit1, codeDigit2, codeDigit3 };
        directionPattern = isLeftRightLeft ?
            new bool[] { true, false, true } : // true = esquerda, false = direita
            new bool[] { false, true, false };

        // Debug.Log("Cofre iniciado. Gire o disco e confirme com Enter.");
    }

    // void Update()
    // {
    //     HandleDialInput();
    //     HandleConfirmation();
    // }

    void HandleDialInput() {
        var playerInput = _playerInputSingleton.Value;
        if (playerInput.y < 0) {
            TurnLeft();
        }
        else if (playerInput.y > 0) {
            TurnRight();
        }
    }

    private void TurnRight() {
        currentDialPosition++;
        lastTurnDirection = Direction.Right;
        WrapDial();
        // Debug.Log($"Disco girado para a DIREITA. Posição atual: {currentDialPosition}");
        audioSource.PlayOneShot(dialTurnClip);
    }

    private void TurnLeft() {
        currentDialPosition--;
        lastTurnDirection = Direction.Left;
        WrapDial();
        // Debug.Log($"Disco girado para a ESQUERDA. Posição atual: {currentDialPosition}");
        audioSource.PlayOneShot(dialTurnClip);
    }

    void WrapDial()
    {
        if (currentDialPosition < 0)
            currentDialPosition = 20;
        else if (currentDialPosition > 20)
            currentDialPosition = 0;
    }

    void HandleConfirmation()
    {
        if (currentInputIndex >= 3)
            return;

        bool expectedLeft = directionPattern[currentInputIndex];
        Direction expectedDirection = expectedLeft ? Direction.Left : Direction.Right;

        if (currentDialPosition == codeSequence[currentInputIndex] && lastTurnDirection == expectedDirection)
        {
            // Debug.Log($"Entrada {currentInputIndex + 1} correta: {currentDialPosition} ({lastTurnDirection})");
            audioSource.PlayOneShot(correctDigitClip);
            currentInputIndex++;

            if (currentInputIndex == 3)
            {
                // Debug.Log("Cofre destrancado com sucesso!");
                audioSource.PlayOneShot(successClip);
                _miniGameEnded.InvokeEvent();
            }
        }
        else
        {
            // Debug.LogWarning("Entrada incorreta! Cofre resetado.");
            audioSource.PlayOneShot(failClip);
            ResetLock();
        }

        // Bloqueia nova confirma��o at� que usu�rio gire o disco de novo
        lastTurnDirection = Direction.None;
    }

    void ResetLock()
    {
        currentInputIndex = 0;
        currentDialPosition = 0;
        lastTurnDirection = Direction.None;
    }
}
