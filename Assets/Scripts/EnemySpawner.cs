using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    // Serializable class to hold enemy variant data
    [System.Serializable]
    public class EnemyVariant
    {
        public GameObject enemyPrefab;
        public float spawnWeight = 1f; // Initial spawn weight
        public float spawnWeightIncrease = 1f; // Weight increase over time
    }

    private int enemyCount = 0; // Current number of enemies
    public int maxEnemies = 1; // Maximum number of enemies allowed
    public List<EnemyVariant> enemyVariants = new List<EnemyVariant>(); // List of enemy variants
    private List<GameObject> spawnedEnemies = new List<GameObject>(); // List of spawned enemies

    private float time;
    public float TimeUntilIncrease = 600; // Time interval for increasing enemies and weights
    public float TimeUntilBoss = 5;
    private float bosstimer;
    public int EnemiesIncrease; // Number of enemies to increase at each interval
    public TextMeshProUGUI timeShow; // Text component to show time
    public float SecondsToShow;
    public int MinutesToShow = 0;

    public float spawnInterval = 3f; // Interval between enemy spawns
    public float nextTimeToSpawn = 0f; // Time for next enemy spawn
    private GameObject player;
    public GameObject enemies;
    [SerializeField] Vector3 spawnArea; // Area where enemies can spawn

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Find player object by tag
    }

    void Update()
    {
        time += Time.deltaTime; // Increment time by delta time
        bosstimer += Time.deltaTime; // Increment boss timer by delta time
        UpdateTime(); // Update displayed time

        // Check if it's time to increase enemies and weights
        if (time >= TimeUntilIncrease)
        {
            maxEnemies += EnemiesIncrease; // Increase max enemies

            foreach (EnemyVariant variant in enemyVariants)
            {
                variant.spawnWeight += variant.spawnWeightIncrease; // Increase spawn weight
            }
            time = 0; // Reset timer
        }

        // Check if it's time to spawn a boss
        if (bosstimer >= TimeUntilBoss)
        {
            bosstimer = 0; // Reset boss timer
            SpawnBoss(); // Spawn a boss
        }

        // Find all enemies currently in the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemies.Length; // Update enemy count

        // Check if it's time to spawn a new enemy
        if (Time.time - nextTimeToSpawn > 1 * spawnInterval && enemyCount < maxEnemies)
        {
            nextTimeToSpawn = Time.time; // Set next spawn time
            SpawnEnemy(); // Spawn a new enemy
        }
    }

    // Method to spawn a regular enemy
    private void SpawnEnemy()
    {
        Vector3 randomSpawnPoint = MakeRandomSpawnPos(); // Generate a random spawn position
        randomSpawnPoint += player.transform.position; // Adjust position relative to player

        EnemyVariant randomEnemyVariant = GetRandomEnemyVariant(); // Get a random enemy variant

        if (randomSpawnPoint != null && randomEnemyVariant != null)
        {
            // Instantiate the enemy and add it to the list of spawned enemies
            GameObject newEnemy = Instantiate(randomEnemyVariant.enemyPrefab, randomSpawnPoint, Quaternion.Euler(randomSpawnPoint), enemies.transform);
            spawnedEnemies.Add(newEnemy);
        }
    }

    // Method to spawn a boss enemy
    private void SpawnBoss()
    {
        Vector3 randomSpawnPoint = MakeRandomSpawnPos(); // Generate a random spawn position
        randomSpawnPoint += player.transform.position; // Adjust position relative to player

        EnemyVariant randomEnemyVariant = GetRandomEnemyVariant(); // Get a random enemy variant

        if (randomSpawnPoint != null && randomEnemyVariant != null)
        {
            // Instantiate the boss enemy with increased stats and mark it as a boss
            GameObject newEnemy = Instantiate(randomEnemyVariant.enemyPrefab, randomSpawnPoint, Quaternion.Euler(randomSpawnPoint), enemies.transform);
            newEnemy.transform.localScale *= 2; // Double the size
            var EnemyScript = newEnemy.GetComponent<EnemyScript>();
            EnemyScript.enemyHealth *= 2.5f;
            EnemyScript.damage *= 2.5f;
            EnemyScript.Boss = true;
        }
    }

    // Method to generate a random spawn position within the spawn area
    private Vector3 MakeRandomSpawnPos()
    {
        Vector3 spawnPosition = new Vector3();

        // Determine if the spawn point should be on the top/bottom or left/right edge
        float f = Random.value > 0.5f ? -1f : 1f;

        if (Random.value > 0.5f)
        {
            spawnPosition.x = Random.Range(-spawnArea.x, spawnArea.x);
            spawnPosition.y = spawnArea.y * f;
        }
        else
        {
            spawnPosition.y = Random.Range(-spawnArea.y, spawnArea.y);
            spawnPosition.x = spawnArea.x * f;
        }

        spawnPosition.z = 0; // Ensure spawn position is in 2D plane

        return spawnPosition;
    }

    // Method to select a random enemy variant based on their spawn weights
    private EnemyVariant GetRandomEnemyVariant()
    {
        if (enemyVariants.Count > 0)
        {
            float totalWeight = 0f; // Total weight of all enemy variants

            foreach (EnemyVariant variant in enemyVariants)
            {
                totalWeight += variant.spawnWeight; // Sum up weights
            }

            float randomValue = Random.Range(0f, totalWeight); // Pick a random value

            foreach (EnemyVariant variant in enemyVariants)
            {
                if (randomValue <= variant.spawnWeight)
                {
                    return variant; // Return the selected variant
                }

                randomValue -= variant.spawnWeight; // Decrease random value by variant's weight
            }
        }

        return null; // Return null if no variants are available
    }

    // Method to update the displayed time
    private void UpdateTime()
    {
        SecondsToShow += Time.deltaTime; // Increment seconds
        if (SecondsToShow >= 60)
        {
            MinutesToShow++; // Increment minutes
            SecondsToShow = 0; // Reset seconds
        }

        // Update the displayed time text
        if (SecondsToShow < 10)
        {
            timeShow.text = MinutesToShow.ToString() + ":0" + ((int)SecondsToShow).ToString();
        }
        else
        {
            timeShow.text = MinutesToShow.ToString() + ":" + ((int)SecondsToShow).ToString();
        }
    }
}

