using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    public static CollectableSpawner instance;

    [Header("Prefabs")]
    [SerializeField] private GameObject correctCollectablePrefab;
    [SerializeField] private GameObject incorrectCollectablePrefab;

    [Header("Spawn Settings")]
    [SerializeField] private Vector2 spawnAreaMin;
    [SerializeField] private Vector2 spawnAreaMax;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    public void Start()
    {
        int spawnAmount = Random.Range(5, 50);
        for (int i = 0; i < spawnAmount; i++)
        {
            SpawnRandomCollectable();
        }
    }

    public void SpawnRandomCollectable()
    {
        GameObject prefabToSpawn = Random.value > 0.5f ? correctCollectablePrefab : incorrectCollectablePrefab;

        Vector3 randomPosition = new Vector3(Random.Range(spawnAreaMin.x, spawnAreaMax.x), 2.5f, Random.Range(spawnAreaMin.y, spawnAreaMax.y));

        Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);
    }
}
