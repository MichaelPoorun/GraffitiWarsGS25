using UnityEngine;

public class EventMainCam1_W : MonoBehaviour
{
    public CameraLocks_W CLW;
    public string Event;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && CLW.P1Activated == true)
        {
            CLW.HandleEvent(Event);
        }

    }
}
