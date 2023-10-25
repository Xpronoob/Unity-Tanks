using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

    public GameObject enemyPrefab;
    [SerializeField]
    public float spawnInterval = 3.0f;
    [SerializeField]
    public float spawnRadius = 30.0f;

    private float nextSpawnTime = 0;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        Vector3 randomPosition = transform.position + Random.insideUnitSphere * spawnRadius;
        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }
}
