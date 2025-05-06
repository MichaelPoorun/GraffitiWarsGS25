using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

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

    public bool readyToCheck;
    public CameraLocks_W CLW;
    public string Event;

    void Start()
    {
        waveManager = FindAnyObjectByType<WaveManager>();
        GenerateWave();
        readyToCheck = false;
        StartCoroutine(Wait());
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

        /*CheckWaveCompletion();// checks if the wave is completed*/
    }
    void Update()
    {
        if (readyToCheck == true)
        {
            activeEnemies.RemoveAll(enemy => enemy == null);

            if (activeEnemies.Count == 0 && enemiesToSpawn.Count == 0)
            {
                CLW.HandleEvent(Event);
            }
        }
    }

    public void GenerateWave()
    {
        waveValue = currWave * 2;
        GenerateEnemies();

        spawnInterval = waveDuration / enemiesToSpawn.Count; //gives the wave a fixed time in which enemies spawn
        waveTimer = waveDuration; 
    }
    
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        readyToCheck = true;
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

    /*private void CheckWaveCompletion()
    {
        activeEnemies.RemoveAll(enemy => enemy == null);

        if (activeEnemies.Count == 0 & enemiesToSpawn.Count == 0)
        {
            waveManager.OnWaveCompleted();
        }
    }*/
}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}
