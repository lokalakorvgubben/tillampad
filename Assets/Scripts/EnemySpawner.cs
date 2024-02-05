using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyVariant
    {
        public GameObject enemyPrefab;
        public float spawnWeight = 1f; // Adjust as needed
    }

    private int enemyCount = 0;
    public int maxEnemies = 1;
    public List<EnemyVariant> enemyVariants = new List<EnemyVariant>();
    private List<GameObject> spawnedEnemies = new List<GameObject>();



    public float spawnInterval = 3f;
    public float nextTimeToSpawn = 0f;
    private GameObject player;
    public GameObject EnemyTest;
    public GameObject enemies;
    [SerializeField] Vector3 spawnArea;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        enemyCount = enemies.Length;
        if(enemyCount < maxEnemies)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector3 randomSpawnPoint = MakeRandomSpawnPos();
        EnemyVariant randomEnemyVariant = GetRandomEnemyVariant();

        if (randomSpawnPoint != null && randomEnemyVariant != null)
        {
            GameObject newEnemy = Instantiate(randomEnemyVariant.enemyPrefab, randomSpawnPoint, Quaternion.Euler(randomSpawnPoint));
            spawnedEnemies.Add(newEnemy);
        }
    }

    private Vector3 MakeRandomSpawnPos()
    {
        Vector3 spawnPosition = new Vector3();

        float f = Random.value > 0.5f ? -1f : 1f;

        if(Random.value > 0.5f)
        {
            spawnPosition.x = Random.Range(-spawnArea.x, spawnArea.x);
            spawnPosition.y = spawnArea.y * f;
        }
        else
        {
            spawnPosition.y = Random.Range(-spawnArea.y, spawnArea.y);
            spawnPosition.x = spawnArea.x * f;
        }

        spawnPosition.z = 0;

        return spawnPosition;
    }

    private EnemyVariant GetRandomEnemyVariant()
    {
        if (enemyVariants.Count > 0)
        {
            float totalWeight = 0f;

            foreach (EnemyVariant variant in enemyVariants)
            {
                totalWeight += variant.spawnWeight;
            }

            float randomValue = Random.Range(0f, totalWeight);

            foreach (EnemyVariant variant in enemyVariants)
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
}
