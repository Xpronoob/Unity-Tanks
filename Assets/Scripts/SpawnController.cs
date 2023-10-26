using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

    public GameObject enemyPrefab;
    [SerializeField]
    public float spawnInterval = 15.0f;
    [SerializeField]
    public float spawnRadius = 30.0f;

    private float nextSpawnTime = 15.0f;
    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnEnemy();
        }
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            for (int i = 0; i < 3; i++)
            {
                SpawnEnemy();
            }
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        Vector3 randomPosition = transform.position + Random.insideUnitSphere * spawnRadius;
        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }
}
