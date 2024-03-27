using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private float time;
    public float TimeUntilIncrease = 600;
    public int EnemiesIncrease;
    public TextMeshProUGUI timeShow;
    private float SecondsToShow;
    private int MinutesToShow = 0;

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
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        UpdateTime();
        if(time >= TimeUntilIncrease)
        {
            maxEnemies += EnemiesIncrease;
            time = 0;
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        enemyCount = enemies.Length;

        if (Time.time - nextTimeToSpawn > 1 * spawnInterval && enemyCount < maxEnemies)
        {
            nextTimeToSpawn = Time.time;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector3 randomSpawnPoint = MakeRandomSpawnPos();
        randomSpawnPoint += player.transform.position;

        EnemyVariant randomEnemyVariant = GetRandomEnemyVariant();

        if (randomSpawnPoint != null && randomEnemyVariant != null)
        {
            GameObject newEnemy = Instantiate(randomEnemyVariant.enemyPrefab, randomSpawnPoint, Quaternion.Euler(randomSpawnPoint), enemies.transform);
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

    private void UpdateTime()
    {
        SecondsToShow += Time.deltaTime;
        if(SecondsToShow >= 60)
        {
            MinutesToShow++;
            SecondsToShow = 0;
        }
        if(SecondsToShow < 10)
        {
            timeShow.text = MinutesToShow.ToString() + ":0" +  ((int)SecondsToShow).ToString();
        }
        else
        {
            timeShow.text = MinutesToShow.ToString() + ":" + ((int)SecondsToShow).ToString();
        }
    }
}
