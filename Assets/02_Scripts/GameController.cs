using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameController : MonoBehaviour
{
    public GameObject cellPrefab; // Prefab de la c�lula.
    public int populationSize = 10; // Tama�o de la poblaci�n.
    List<GameObject> population = new List<GameObject>(); // Lista de la poblaci�n actual.
    public static float elapsed = 0; // Tiempo transcurrido en la ronda.
    int trialTime = 10; // Duraci�n de cada ronda en segundos.
    int generation = 1; // N�mero de generaci�n actual.

    // Estilo del texto que se mostrar� en pantalla.
    GUIStyle guiStyle = new GUIStyle();

    // Mostrar en pantalla el n�mero de ronda y el tiempo.
    private void OnGUI()
    {
        guiStyle.fontSize = 50;
        guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "Round: " + generation, guiStyle);
        GUI.Label(new Rect(10, 65, 100, 20), "Time: " + (int)elapsed, guiStyle);
    }

    void Start()
    {
        // Crear la poblaci�n inicial con posiciones, colores y tama�os aleatorios.
        for (int i = 0; i < populationSize; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(-3.6f, 3.6f), 0);
            GameObject go = Instantiate(cellPrefab, pos, Quaternion.identity);
            go.GetComponent<CellBehaviour>().r = Random.Range(0.0f, 1.0f);
            go.GetComponent<CellBehaviour>().g = Random.Range(0.0f, 1.0f);
            go.GetComponent<CellBehaviour>().b = Random.Range(0.0f, 1.0f);
            go.GetComponent<CellBehaviour>().s = Random.Range(0.2f, 1.0f);
            population.Add(go);
        }
    }

    // Cruza dos padres para generar un descendiente.
    GameObject Breed(GameObject parent1, GameObject parent2)
    {
        Vector3 pos = new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(-3.6f, 3.6f), 0);
        GameObject offspring = Instantiate(cellPrefab, pos, Quaternion.identity);
        CellBehaviour dna1 = parent1.GetComponent<CellBehaviour>();
        CellBehaviour dna2 = parent2.GetComponent<CellBehaviour>();

        // Selecci�n aleatoria de los genes de los padres para los descendientes, con posibilidad de mutaci�n.
        if (Random.Range(0, 1000) > 5) // Evitar mutaci�n.
        {
            offspring.GetComponent<CellBehaviour>().r = Random.Range(0, 10) < 5 ? dna1.r : dna2.r;
            offspring.GetComponent<CellBehaviour>().g = Random.Range(0, 10) < 5 ? dna1.g : dna2.g;
            offspring.GetComponent<CellBehaviour>().b = Random.Range(0, 10) < 5 ? dna1.b : dna2.b;
            offspring.GetComponent<CellBehaviour>().s = Random.Range(0, 10) < 5 ? dna1.s : dna2.s;
        }
        else
        {
            // Mutaci�n aleatoria si ocurre.
            offspring.GetComponent<CellBehaviour>().r = Random.Range(0.0f, 1.0f);
            offspring.GetComponent<CellBehaviour>().g = Random.Range(0.0f, 1.0f);
            offspring.GetComponent<CellBehaviour>().b = Random.Range(0.0f, 1.0f);
            offspring.GetComponent<CellBehaviour>().s = Random.Range(0.2f, 1.0f);
        }
        return offspring;
    }

    // Crea una nueva generaci�n de c�lulas basada en la mitad superior m�s apta.
    void BreedNewPopulation()
    {
        List<GameObject> newPopulation = new List<GameObject>();

        // Ordenar la poblaci�n seg�n el tiempo que sobrevivieron (los m�s aptos viven m�s).
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<CellBehaviour>().timeToDie).ToList();
        population.Clear();

        // Cruzar la mitad superior de la poblaci�n para crear descendientes.
        for (int i = (int)(sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }

        // Destruir la poblaci�n anterior.
        for (int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }
        generation++; // Incrementar el n�mero de generaci�n.
    }

    void Update()
    {
        elapsed += Time.deltaTime; // Incrementar el tiempo transcurrido.
        if (elapsed > trialTime) // Si el tiempo excede el tiempo de prueba.
        {
            BreedNewPopulation(); // Crear nueva generaci�n.
            elapsed = 0; // Reiniciar el tiempo.
        }
    }
}
