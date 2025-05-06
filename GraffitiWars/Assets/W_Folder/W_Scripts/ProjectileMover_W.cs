using UnityEngine;

public class ProjectileMover_W : MonoBehaviour
{
    private float speed;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
        Destroy(gameObject, 5f); // Destroy after 5 seconds
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
