using UnityEngine;

public class EventPBoss_W : MonoBehaviour
{
    public CameraLocks_W CLW;
    public string Event;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && CLW.PBossActivated == false)
        {
            CLW.PBossActivated = true;
            CLW.HandleEvent(Event);
        }

    }
}
