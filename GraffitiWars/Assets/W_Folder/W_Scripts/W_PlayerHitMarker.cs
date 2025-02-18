using UnityEngine;

public class W_PlayerHitMarker : MonoBehaviour
{
    public GameObject AttackBox;
    public BoxCollider BoxCollider;
    
    void Start()
    {
        BoxCollider = AttackBox.GetComponent<BoxCollider>();
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
