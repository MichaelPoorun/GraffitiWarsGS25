using UnityEngine;
using System.Collections;

public class TrainMovement_W : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float acceleration = 1f;
    public float deceleration = 1f;
    public float stopDuration = 2f;

    private float currentSpeed = 10f;

    public bool delTrain = false;

    void Start()
    {
        StartCoroutine(TrainRoutine());
    }

    void Update()
    {
        transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);
    }

    IEnumerator TrainRoutine()
    {
        // Accelerate
        while (currentSpeed < maxSpeed)
        {
            currentSpeed += acceleration * Time.deltaTime;
            yield return null;
        }

        currentSpeed = maxSpeed;
        yield return new WaitForSeconds(3.5f);

        // Decelerate
        while (currentSpeed > 0f)
        {
            currentSpeed -= deceleration * Time.deltaTime;
            yield return null;
        }

        currentSpeed = 0f;
        yield return new WaitForSeconds(stopDuration);

        // Accelerate again
        while (currentSpeed < maxSpeed)
        {
            currentSpeed += acceleration * Time.deltaTime;
            yield return null;
        }

        currentSpeed = maxSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DelTrain"))
        {
            delTrain = true;
        }
    }
}
