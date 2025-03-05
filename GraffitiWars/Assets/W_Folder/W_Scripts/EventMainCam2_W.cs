using UnityEngine;

public class EventMainCam2_W : MonoBehaviour
{
    public CameraLocks_W CLW;
    public string Event;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && CLW.P2Activated == true)
        {
            CLW.HandleEvent(Event);
        }

    }
}
