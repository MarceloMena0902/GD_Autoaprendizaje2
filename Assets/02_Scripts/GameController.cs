using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameController : MonoBehaviour
{
    public GameObject cellPrefab; // Prefab de la célula.
    public int populationSize = 10; // Tamaño de la población.
    List<GameObject> population = new List<GameObject>(); // Lista de la población actual.
    public static float elapsed = 0; // Tiempo transcurrido en la ronda.
    int trialTime = 10; // Duración de cada ronda en segundos.
    int generation = 1; // Número de generación actual.

    // Estilo del texto que se mostrará en pantalla.
    GUIStyle guiStyle = new GUIStyle();

    // Mostrar en pantalla el número de ronda y el tiempo.
    private void OnGUI()
    {
        guiStyle.fontSize = 50;
        guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "Round: " + generation, guiStyle);
        GUI.Label(new Rect(10, 65, 100, 20), "Time: " + (int)elapsed, guiStyle);
    }

    void Start()
    {
        // Crear la población inicial con posiciones, colores y tamaños aleatorios.
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

        // Selección aleatoria de los genes de los padres para los descendientes, con posibilidad de mutación.
        if (Random.Range(0, 1000) > 5) // Evitar mutación.
        {
            offspring.GetComponent<CellBehaviour>().r = Random.Range(0, 10) < 5 ? dna1.r : dna2.r;
            offspring.GetComponent<CellBehaviour>().g = Random.Range(0, 10) < 5 ? dna1.g : dna2.g;
            offspring.GetComponent<CellBehaviour>().b = Random.Range(0, 10) < 5 ? dna1.b : dna2.b;
            offspring.GetComponent<CellBehaviour>().s = Random.Range(0, 10) < 5 ? dna1.s : dna2.s;
        }
        else
        {
            // Mutación aleatoria si ocurre.
            offspring.GetComponent<CellBehaviour>().r = Random.Range(0.0f, 1.0f);
            offspring.GetComponent<CellBehaviour>().g = Random.Range(0.0f, 1.0f);
            offspring.GetComponent<CellBehaviour>().b = Random.Range(0.0f, 1.0f);
            offspring.GetComponent<CellBehaviour>().s = Random.Range(0.2f, 1.0f);
        }
        return offspring;
    }

    // Crea una nueva generación de células basada en la mitad superior más apta.
    void BreedNewPopulation()
    {
        List<GameObject> newPopulation = new List<GameObject>();

        // Ordenar la población según el tiempo que sobrevivieron (los más aptos viven más).
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<CellBehaviour>().timeToDie).ToList();
        population.Clear();

        // Cruzar la mitad superior de la población para crear descendientes.
        for (int i = (int)(sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }

        // Destruir la población anterior.
        for (int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }
        generation++; // Incrementar el número de generación.
    }

    void Update()
    {
        elapsed += Time.deltaTime; // Incrementar el tiempo transcurrido.
        if (elapsed > trialTime) // Si el tiempo excede el tiempo de prueba.
        {
            BreedNewPopulation(); // Crear nueva generación.
            elapsed = 0; // Reiniciar el tiempo.
        }
    }
}
