using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopulationManager_sc : MonoBehaviour
{
    public GameObject personPrefab; // Reference to the person object
    public int populationSize = 10; // Population size
    public static float elapsed = 0; // Time tracking
    private List<GameObject> population = new List<GameObject>(); // List of population
    private int trialTime = 5; // Duration of each cycle
    private int generation = 1; // Generation count
    GUIStyle guiStyle = new GUIStyle(); // GUI text style

    // Creates the population at the start
    void Start()
    {
        for (int i = 0; i < populationSize; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-9.5f, 9.5f), Random.Range(-3.4f, 5.4f), 0);
            GameObject o = Instantiate(personPrefab, pos, Quaternion.identity);
            o.GetComponent<DNA>().r = Random.Range(0.0f, 1.0f);
            o.GetComponent<DNA>().g = Random.Range(0.0f, 1.0f);
            o.GetComponent<DNA>().b = Random.Range(0.0f, 1.0f);
            population.Add(o);
        }
    }

    // Updates the time for each generation and changes the generation
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > trialTime)
        {
            BreedNewPopulation(); // Create a new generation
            elapsed = 0;
        }
    }

    // Creates a new generation
    void BreedNewPopulation()
    {
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<DNA>().timeToDie).ToList();
        population.Clear();

        for (int i = (int)(sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }

        for (int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]); // Destroy old objects
        }

        generation++;
    }

    // Creates a new offspring from two parents
    GameObject Breed(GameObject parent1, GameObject parent2)
    {
        Vector3 pos = new Vector3(Random.Range(-9.5f, 9.5f), Random.Range(-3.4f, 5.4f), 0);
        GameObject offspring = Instantiate(personPrefab, pos, Quaternion.identity);
        DNA dna1 = parent1.GetComponent<DNA>();
        DNA dna2 = parent2.GetComponent<DNA>();

        offspring.GetComponent<DNA>().r = Random.Range(0, 10) < 5 ? dna1.r : dna2.r;
        offspring.GetComponent<DNA>().g = Random.Range(0, 10) < 5 ? dna1.g : dna2.g;
        offspring.GetComponent<DNA>().b = Random.Range(0, 10) < 5 ? dna1.b : dna2.b;

        return offspring;
    }

    // Displays generation and time information on the screen
    void OnGUI()
    {
        guiStyle.fontSize = 20;
        guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "Generation: " + generation, guiStyle);
        GUI.Label(new Rect(10, 30, 100, 20), "Time: " + (int)elapsed, guiStyle);
    }
}
