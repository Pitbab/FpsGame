using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetSpawner : MonoBehaviour
{
    private BoxCollider boxCol;
    [SerializeField] private GameObject targetPrefab;

    private float maxUpTime = 5f;
    
    private float spawnRate = 2f;
    private float currentTime = 0f;
    private Vector3 position;
    private Vector3 boxSize;
    private Vector3 spawnPos;

    private void Start()
    {
        boxCol = GetComponent<BoxCollider>();
        position = transform.position;
        boxSize = boxCol.size;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (!(currentTime > spawnRate)) return;
        
        SpawnTarget();
        currentTime = 0f;
    }

    private void SpawnTarget()
    {

        float randomX = Random.Range(position.x - boxSize.x/2, position.x + boxSize.x/2);
        float randomY = Random.Range(position.y - boxSize.y/2, position.y + boxSize.y/2);
        float randomZ = Random.Range(position.z - boxSize.z/2, position.z + boxSize.z/2);

        spawnPos = new Vector3(randomX, randomY, randomZ);

        Destroy(Instantiate(targetPrefab, spawnPos, Quaternion.identity), maxUpTime);
    }
}
