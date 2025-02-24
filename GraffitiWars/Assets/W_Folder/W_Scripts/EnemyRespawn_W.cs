using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;

public class EnemyRespawn_W : MonoBehaviour
{
    public GameObject Enemy;
    public Transform spawnPoint;
    private GameObject currentEnemy;
    private bool isSpawning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnEnemy();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEnemy == null && !isSpawning)
        {
            StartCoroutine(WaitSpawn());
        }
    }

    void SpawnEnemy()
    {
        currentEnemy = Instantiate(Enemy, spawnPoint.position, Quaternion.Euler(0,90,0));
        isSpawning = false;
    }

    IEnumerator WaitSpawn()
    {
        isSpawning = true;
        yield return new WaitForSeconds(1f);
        SpawnEnemy();
    }
}
