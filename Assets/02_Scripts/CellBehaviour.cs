using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBehaviour : MonoBehaviour
{
    public float r; // Valor del canal rojo (RGB) de la célula.
    public float g; // Valor del canal verde (RGB) de la célula.
    public float b; // Valor del canal azul (RGB) de la célula.
    public float s; // Tamaño de la célula.

    bool dead = false; // Si la célula está muerta.
    public float timeToDie = 0; // Momento en el que la célula fue destruida.
    SpriteRenderer sRenderer; // Renderizador para mostrar el sprite de la célula.
    Collider2D sCollider; // Collider para detectar los clics del jugador.

    // Cuando se hace clic en la célula, se marca como muerta y se guarda el tiempo de muerte.
    void OnMouseDown()
    {
        dead = true;
        timeToDie = GameController.elapsed;
        sRenderer.enabled = false; // Ocultar el sprite.
        sCollider.enabled = false; // Desactivar las colisiones.
    }

    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>(); // Obtener el renderizador de la célula.
        sCollider = GetComponent<Collider2D>(); // Obtener el collider de la célula.
        sRenderer.color = new Color(r, g, b); // Asignar el color basado en los valores RGB.
        this.transform.localScale = new Vector3(s, s, s); // Asignar el tamaño de la célula.
    }
}
