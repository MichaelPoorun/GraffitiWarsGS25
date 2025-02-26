using UnityEngine;

public class EventBoxScript : MonoBehaviour
{
    public CameraLocks_W CLW;
    public string Event;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CLW.HandleEvent(Event);
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        }
    }
}
