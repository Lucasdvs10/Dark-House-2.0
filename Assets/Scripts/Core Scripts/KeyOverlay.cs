using UnityEngine;
using UnityEngine.UI;

public class KeyOverlay : MonoBehaviour
{
    // Sprites das teclas
    public Sprite whiteW, blackW;
    public Sprite whiteA, blackA;
    public Sprite whiteS, blackS;
    public Sprite whiteD, blackD;

    // Imagens no HUD
    public Image wImage, aImage, sImage, dImage;

    void Update()
    {
        wImage.sprite = Input.GetKey(KeyCode.W) ? blackW : whiteW;
        aImage.sprite = Input.GetKey(KeyCode.A) ? blackA : whiteA;
        sImage.sprite = Input.GetKey(KeyCode.S) ? blackS : whiteS;
        dImage.sprite = Input.GetKey(KeyCode.D) ? blackD : whiteD;
    }
}
