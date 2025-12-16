using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Configuración del Spawner")]
    public Transform puntoDeSpawn;  // El lugar donde nacerán
    public float spawnDelay = 3.0f; // Tiempo entre objetos (Sugerencia: auméntalo un poco)

    [Header("Lista de Objetos")]
    // Aquí arrastraremos tus 3 frutas y 3 juguetes
    public GameObject[] objetosParaSpawnear;

    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            // 1. Espera el tiempo de delay
            yield return new WaitForSeconds(spawnDelay);

            // 2. Elige un número al azar entre 0 y el total de objetos (6)
            int indiceAleatorio = Random.Range(0, objetosParaSpawnear.Length);

            // 3. Obtiene el prefab correspondiente a ese número
            GameObject objetoElegido = objetosParaSpawnear[indiceAleatorio];

            // 4. Lo crea en la escena
            Instantiate(objetoElegido, puntoDeSpawn.position, puntoDeSpawn.rotation);
        }
    }
}