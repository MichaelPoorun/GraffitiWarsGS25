using UnityEngine;
using System.Collections;

public class TrainManager : MonoBehaviour
{
    public GameObject trainPrefab;
    public Transform spawnPoint;
    public float respawnDelay;

    private GameObject currentTrain;
    private TrainMovement_W trainMovement;

    void Start()
    {
        StartCoroutine(RespawnTrainAfterDelay());
    }

    void Update()
    {
        if (currentTrain != null && trainMovement != null && trainMovement.delTrain == true)
        {
            Destroy(currentTrain);
            currentTrain = null;
            trainMovement = null;
            StartCoroutine(RespawnTrainAfterDelay());
            trainMovement.delTrain = false;
        }
    }

    void SpawnTrain()
    {
        currentTrain = Instantiate(trainPrefab, spawnPoint.position, Quaternion.identity);
        trainMovement = currentTrain.GetComponent<TrainMovement_W>();
    }

    IEnumerator RespawnTrainAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnTrain();
        yield return new WaitForSeconds(1f);
        respawnDelay = 20f;
    }
}
