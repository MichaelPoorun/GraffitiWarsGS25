using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player object
    public Vector3 offset = new Vector3(0, 5, -10); // Camera position relative to the player
    public float smoothSpeed = 0.125f; // How smoothly the camera follows the player

    void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogWarning("Player not assigned in CameraFollow script.");
            return;
        }

        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(player); // Ensure the camera is always looking at the player
    }
}

