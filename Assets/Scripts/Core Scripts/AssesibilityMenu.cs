using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class AssesibilityMenu : MonoBehaviour
{
    [SerializeField] private List<Selectable> buttons; 
    private int currentIndex = 0;

    void Start()
    {
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
        }
    }

    private void SelectButton(int index)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttons[index].gameObject);
    }
}
