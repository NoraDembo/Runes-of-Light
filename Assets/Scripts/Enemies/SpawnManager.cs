using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float minTime = 1.0f;
    public float maxTime = 10.0f;
    public int maxEnemies;

    public float probabilityGrowthFactor;

    public Transform[] spawnPoints;

    GameObject[] enemies;
    public GameObject target;

    float spawnTimer;
    float nextSpawn;
    public static int spawnedEnemies;

    void Start()
    {
        enemies = GameManager.enemies;
        target = GameObject.Find("Character");
        nextSpawn = minTime + Random.Range(minTime, maxTime);
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        minTime = 1 + 5 * Mathf.Exp(-0.02f * Time.timeSinceLevelLoad);
        maxTime = 1 + 10 * Mathf.Exp(-0.02f * Time.timeSinceLevelLoad);

        if (spawnTimer > nextSpawn && spawnedEnemies < maxEnemies)
        {
            float timePassed = Time.timeSinceLevelLoad;
            float factor = (-enemies.Length * Mathf.Exp(-probabilityGrowthFactor * timePassed)) + enemies.Length;
            float enemyType = factor * Mathf.Pow(Random.Range(0f, 1f), 2);

            //determine random spawn point out of spawn point list
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // if spawnPoint is not on the screen, spawn an enemy
            Vector3 viewPos = Camera.main.WorldToViewportPoint(spawnPoint.position);
            if (!(viewPos.x > 0 && viewPos.x > 1) || !(viewPos.y > 0 && viewPos.y > 1))
            {
                Spawn((int)Mathf.Floor(enemyType), spawnPoint);
            }
        }
    }

    void Spawn(int enemyType, Transform spawnPoint)
    {
            GameObject spawn = Instantiate(enemies[enemyType], spawnPoint.position, spawnPoint.rotation);
            spawn.GetComponent<EnemyController>().attackTarget = target;
            spawnedEnemies++;

            //spawn successful, reset timer and get spawn time for the next enemy
            spawnTimer = 0;
            nextSpawn = minTime + Random.Range(minTime, maxTime);    }
}
