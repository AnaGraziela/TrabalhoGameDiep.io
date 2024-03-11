using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedical : MonoBehaviour
{

    public GameObject objectToSpawn; // Objeto que será instanciado
    public Vector3 spawnAreaCenter; // Centro da área de spawn
    public Vector3 spawnAreaSize;   // Tamanho da área de spawn
    public float minSpawnInterval = 1f; // Intervalo mínimo entre spawns
    public float maxSpawnInterval = 5f;
    private float nextSpawnTime; // Próximo momento para spawnar
    void Start()
    {
        // Define o próximo momento para spawnar
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        // Verifica se é o momento para spawnar
        if (Time.time >= nextSpawnTime)
        {
            // Calcula uma posição aleatória dentro da área de spawn
            Vector3 randomPosition = spawnAreaCenter + new Vector3(Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
                                                                   Random.Range(-spawnAreaSize.y / 2f, spawnAreaSize.y / 2f),
                                                                   Random.Range(-spawnAreaSize.z / 2f, spawnAreaSize.z / 2f));

            // Instancia o objeto na posição aleatória
            Instantiate(objectToSpawn, randomPosition, Quaternion.identity);

            // Define o próximo momento para spawnar
            nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }
}
