using UnityEngine;

public class EventP3_W : MonoBehaviour
{
    public CameraLocks_W CLW;
    public string Event;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && CLW.P3Activated == false)
        {
            CLW.P3Activated = true;
            CLW.HandleEvent(Event);
        }

    }
}
