using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public int currWave;
    public int waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    public List<GameObject> activeEnemies = new List<GameObject>(); // tracks active enemies

    public List<Transform> spawnLocations;
    public int waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;
    private WaveManager waveManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waveManager = FindAnyObjectByType<WaveManager>();
        GenerateWave();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(spawnTimer <= 0)
        {
            if(enemiesToSpawn.Count > 0)
            {
                Transform randomSpawnPoint = spawnLocations[Random.Range(0, spawnLocations.Count)];
                GameObject newEnemy = Instantiate(enemiesToSpawn[0], randomSpawnPoint.position, Quaternion.identity);//spawns the first enemy within the list
                enemiesToSpawn.RemoveAt(0); //removes the enemy from the list
                activeEnemies.Add(newEnemy);
                spawnTimer = spawnInterval;
            }
            else
            {
                waveTimer = 0;
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }

        CheckWaveCompletion();// checks if the wave is completed
    }

    public void GenerateWave()
    {
        waveValue = currWave * 5;
        GenerateEnemies();

        spawnInterval = waveDuration / enemiesToSpawn.Count; //gives the wave a fixed time in which enemies spawn
        waveTimer = waveDuration; 
    }

    public void GenerateEnemies()
    {
        List<GameObject> generatedEnemies = new List<GameObject>();
        while (waveValue > 0)
        {
            int randEnemyID = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyID].cost;

            if (waveValue - randEnemyID >= 0)
            {
                generatedEnemies.Add(enemies[randEnemyID].enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else
            {
                break;
            }
        }

        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }

    private void CheckWaveCompletion()
    {
        activeEnemies.RemoveAll(enemy => enemy == null);

        if (activeEnemies.Count == 0 & enemiesToSpawn.Count == 0)
        {
            waveManager.OnWaveCompleted();
        }
    }
}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}
