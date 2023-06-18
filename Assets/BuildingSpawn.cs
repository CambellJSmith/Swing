using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawn : MonoBehaviour
{
    public GameObject prefab;
    public float minSpawnOffset = 3f;
    public float maxSpawnOffset = 10f;
    public float minYPosition = -5f;
    public float maxYPosition = 10f;
    public float zPosition = 0f;
    public float spawnInterval = 0.01f;
    public Transform playerTransform;
    public float maxDistance = 30f;

    private void Start()
    {
        StartCoroutine(SpawnPrefabRepeatedly());
    }

    private System.Collections.IEnumerator SpawnPrefabRepeatedly()
    {
        while (true)
        {
            SpawnPrefab();

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnPrefab()
    {
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");
        float maxX = float.MinValue;
        GameObject maxBuilding = null;

        foreach (GameObject building in buildings)
        {
            float x = building.transform.position.x;
            if (x > maxX)
            {
                maxX = x;
                maxBuilding = building;
            }
        }

        if (maxBuilding != null)
        {
            float spawnOffset = Random.Range(minSpawnOffset, maxSpawnOffset);
            float spawnX = maxX + spawnOffset;
            float spawnY = Random.Range(minYPosition, maxYPosition);
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, zPosition);

            // Check the distance between the calculated spawn position and the player's position
            float distance = Vector3.Distance(spawnPosition, playerTransform.position);
            if (distance <= maxDistance)
            {
                Instantiate(prefab, spawnPosition, Quaternion.identity);
            }
        }
        else
        {
            Debug.LogWarning("No game object with the tag 'Building' found.");
        }
    }
}
