using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;

public class ObjectSpawner : MonoBehaviour
{
    [System.Serializable]
    public class ObjectVariant
    {
        public GameObject ObjectPrefab;
        public float spawnWeight = 1f;
    }

    public int maxObjects = 1;
    public List<ObjectVariant> objectVariants = new List<ObjectVariant>();
    private List<GameObject> spawnedObjects = new List<GameObject>();
    private GameObject ground;

    private void Start()
    {
        ground = GameObject.Find("Grass");
    }
    private void Update()
    {
        Debug.Log("Spawned = " + spawnedObjects.Count);
        if(spawnedObjects.Count < maxObjects)
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        ObjectVariant randomObjectVariant = GetRandomObjectVariant();
        Vector3 spawn = GetRandomSpawn();
        if (randomObjectVariant != null)
        {
            Debug.Log("Spawn");
            GameObject spawnedObject = Instantiate(randomObjectVariant.ObjectPrefab, spawn, Quaternion.identity);
            spawnedObjects.Add(spawnedObject);
        }
    }


    private ObjectVariant GetRandomObjectVariant()
    {
        if (objectVariants.Count > 0)
        {
            float totalWeight = 0f;

            foreach (ObjectVariant variant in objectVariants)
            {
                totalWeight += variant.spawnWeight;
            }

            float randomValue = Random.Range(0f, totalWeight);

            foreach (ObjectVariant variant in objectVariants)
            {
                if (randomValue <= variant.spawnWeight)
                {
                    return variant;
                }

                randomValue -= variant.spawnWeight;
            }
        }

        return null;
    }

    private Vector3 GetRandomSpawn()
    {
        // Get the size of the ground sprite
        Vector2 groundSize = ground.GetComponent<SpriteRenderer>().bounds.size;

        // Calculate random positions within the bounds of the ground
        float ranX = Random.Range(-groundSize.x / 2f, groundSize.x / 2f);
        float ranY = Random.Range(-groundSize.y / 2f, groundSize.y / 2f);

        Vector3 spawnPosition = ground.transform.position + new Vector3(ranX, ranY, 0);

        return spawnPosition;
    }


}
