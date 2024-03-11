using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedical : MonoBehaviour
{

    public GameObject objectToSpawn; // Objeto que ser� instanciado
    public Vector3 spawnAreaCenter; // Centro da �rea de spawn
    public Vector3 spawnAreaSize;   // Tamanho da �rea de spawn
    public float minSpawnInterval = 1f; // Intervalo m�nimo entre spawns
    public float maxSpawnInterval = 5f;
    private float nextSpawnTime; // Pr�ximo momento para spawnar
    void Start()
    {
        // Define o pr�ximo momento para spawnar
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        // Verifica se � o momento para spawnar
        if (Time.time >= nextSpawnTime)
        {
            // Calcula uma posi��o aleat�ria dentro da �rea de spawn
            Vector3 randomPosition = spawnAreaCenter + new Vector3(Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
                                                                   Random.Range(-spawnAreaSize.y / 2f, spawnAreaSize.y / 2f),
                                                                   Random.Range(-spawnAreaSize.z / 2f, spawnAreaSize.z / 2f));

            // Instancia o objeto na posi��o aleat�ria
            Instantiate(objectToSpawn, randomPosition, Quaternion.identity);

            // Define o pr�ximo momento para spawnar
            nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }
}
