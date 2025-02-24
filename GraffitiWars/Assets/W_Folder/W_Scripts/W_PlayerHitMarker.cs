using UnityEngine;

public class W_PlayerHitMarker : MonoBehaviour
{
    public GameObject AttackBox;
    
    
    void Start()
    {
        
    }

    private void Update()
    {

    }

    public void TurnOffBox()
    {
        AttackBox.SetActive(false);
    }
    public void TurnOnBox()
    {
        AttackBox.SetActive(true);
    }


}
