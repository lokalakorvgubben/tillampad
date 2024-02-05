using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
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
        //Debug.Log(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - nextTimeToSpawn > 1 * spawnInterval)
        {
            nextTimeToSpawn = Time.time;

            Vector3 spawnPosition = MakeRandomSpawnPos();

            spawnPosition += player.transform.position;
            GameObject newEnemy = Instantiate(EnemyTest, enemies.transform);
            newEnemy.transform.position = spawnPosition;

            //Debug.Log("spawn");
            //Debug.Log(player.transform.position);

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
}
