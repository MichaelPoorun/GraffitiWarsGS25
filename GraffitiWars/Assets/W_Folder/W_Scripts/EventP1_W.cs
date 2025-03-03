using UnityEngine;

public class EventP1_W : MonoBehaviour
{
    public CameraLocks_W CLW;
    public string Event;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && CLW.P1Activated == false)
        {
            CLW.P1Activated = true;
            CLW.HandleEvent(Event);
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        }

    }
}
