using UnityEngine;
using UnityEngine.UI;

public class ColorPalette : MonoBehaviour
{
    public Camera mainCamera; // Referencia a la cámara principal.

    // Declaración de los botones para los colores.
    public Button buttonGrisClaro;
    public Button buttonGrisVerdoso;
    public Button buttonGrisMedio;
    public Button buttonBeigeClaro;
    public Button buttonVerdeSuave;

    void Start()
    {
        // Asociar los botones con funciones que cambian el color de fondo cuando se hace clic.
        buttonGrisClaro.onClick.AddListener(() => ChangeBackgroundColor(new Color32(0xE7, 0xE8, 0xE6, 0xFF)));
        buttonGrisVerdoso.onClick.AddListener(() => ChangeBackgroundColor(new Color32(0xC1, 0xC5, 0xC0, 0xFF)));
        buttonGrisMedio.onClick.AddListener(() => ChangeBackgroundColor(new Color32(0x8E, 0x8E, 0x8E, 0xFF)));
        buttonBeigeClaro.onClick.AddListener(() => ChangeBackgroundColor(new Color32(0xFE, 0xF1, 0xE6, 0xFF)));
        buttonVerdeSuave.onClick.AddListener(() => ChangeBackgroundColor(new Color32(0xDF, 0xE6, 0xBF, 0xFF)));
    }

    // Cambia el color de fondo de la cámara.
    void ChangeBackgroundColor(Color32 color)
    {
        mainCamera.backgroundColor = color;
    }
}
