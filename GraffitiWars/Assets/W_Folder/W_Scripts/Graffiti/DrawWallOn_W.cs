using UnityEngine;

public class DrawWallOn_W : MonoBehaviour
{
    public WallHighlight_W wall;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.CompareTag("Player"))
        {
            wall.ActivateWall();
        }
    }
}
