using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class AssesibilityMenu : MonoBehaviour
{
    [SerializeField] private List<Selectable> buttons;
    private int currentIndex = -1;

    [SerializeField] private List<AudioClip> audioClips;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {   
        PlayAudioClip(0);
        if (buttons.Count > 0)
        {
            currentIndex = 0;
            SelectButton(currentIndex);
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            bool shiftHeld = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

            if (currentIndex == -1)
            {
                currentIndex = 0;
            }
            else
            {
                if (shiftHeld)
                {
                    currentIndex--;
                    if (currentIndex < 0) currentIndex = buttons.Count - 1;
                }
                else
                {
                    currentIndex++;
                    if (currentIndex >= buttons.Count) currentIndex = 0;
                }
                SelectButton(currentIndex);
                PlayAudioClip(currentIndex + 1);
            }

            if (currentIndex != -1 && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)))
            {
                var button = buttons[currentIndex].GetComponent<Button>();
                button?.onClick.Invoke();
            }

        }
    }

    private void SelectButton(int index)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttons[index].gameObject);
        buttons[index].Select();
    }

    private void PlayAudioClip(int index)
    {
        if (audioSource != null && audioClips != null && index < audioClips.Count)
        {
            audioSource.Stop();
            audioSource.clip = audioClips[index];
            audioSource.Play();
        }
    }
}
