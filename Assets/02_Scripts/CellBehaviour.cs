using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBehaviour : MonoBehaviour
{
    public float r; // Valor del canal rojo (RGB) de la c�lula.
    public float g; // Valor del canal verde (RGB) de la c�lula.
    public float b; // Valor del canal azul (RGB) de la c�lula.
    public float s; // Tama�o de la c�lula.

    bool dead = false; // Si la c�lula est� muerta.
    public float timeToDie = 0; // Momento en el que la c�lula fue destruida.
    SpriteRenderer sRenderer; // Renderizador para mostrar el sprite de la c�lula.
    Collider2D sCollider; // Collider para detectar los clics del jugador.

    // Cuando se hace clic en la c�lula, se marca como muerta y se guarda el tiempo de muerte.
    void OnMouseDown()
    {
        dead = true;
        timeToDie = GameController.elapsed;
        sRenderer.enabled = false; // Ocultar el sprite.
        sCollider.enabled = false; // Desactivar las colisiones.
    }

    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>(); // Obtener el renderizador de la c�lula.
        sCollider = GetComponent<Collider2D>(); // Obtener el collider de la c�lula.
        sRenderer.color = new Color(r, g, b); // Asignar el color basado en los valores RGB.
        this.transform.localScale = new Vector3(s, s, s); // Asignar el tama�o de la c�lula.
    }
}
