using System.Collections;
using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    public Transform startPoint; // Start position of the train
    public Transform endPoint;   // End position of the train
    public float speed = 5f;     // Speed of the train
    public float respawnTime = 3f; // Time before train reappears

    private Vector3 moveDirection;
    private bool isMoving = true;
    public GameObject Body;

    void Start()
    {
        ResetTrain();
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position += moveDirection * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, endPoint.position) < 0.5f)
            {
                StartCoroutine(RespawnTrain());
            }
        }
    }

    IEnumerator RespawnTrain()
    {
        isMoving = false; // Stop movement
        yield return new WaitForSeconds(0.5f); // Small delay before hiding
        Body.gameObject.SetActive(false); // Hide the train
        yield return new WaitForSeconds(respawnTime); // Wait for respawn time

        // Reset train position before showing it again
        transform.position = startPoint.position;
        moveDirection = (endPoint.position - startPoint.position).normalized;
        
        Body.gameObject.SetActive(true); // Show train again
        isMoving = true; // Resume movement
    }

    void ResetTrain()
    {
        transform.position = startPoint.position; // Move train back to start
        moveDirection = (endPoint.position - startPoint.position).normalized; // Set direction
        gameObject.SetActive(true); // Ensure the train is visible at the start
        isMoving = true;
    }
}