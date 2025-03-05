using UnityEngine;

public class EventMainCam3_W : MonoBehaviour
{
    public CameraLocks_W CLW;
    public string Event;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && CLW.P3Activated == true)
        {
            CLW.HandleEvent(Event);
        }

    }
}
