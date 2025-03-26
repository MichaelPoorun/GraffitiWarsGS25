using System.Collections;
using UnityEngine;

public class TrainRespawner : MonoBehaviour
{
    public GameObject Train;
    public Transform StartPoint;
    private GameObject currentTrain;
    private bool isSpawning = false;

    void Start()
    {
        SpawnTrain();
    }

    void Update()
    {
        if (currentTrain == null && !isSpawning)
        {
            StartCoroutine(WaitSpawn());
        }
    }

    void SpawnTrain()
    {
        currentTrain = Instantiate(Train, StartPoint.position, Quaternion.Euler(0,90,0));
        isSpawning = false;
    }

    IEnumerator WaitSpawn()
    {
        isSpawning = true;
        yield return new WaitForSeconds(1f);
        SpawnTrain();
    }
}
