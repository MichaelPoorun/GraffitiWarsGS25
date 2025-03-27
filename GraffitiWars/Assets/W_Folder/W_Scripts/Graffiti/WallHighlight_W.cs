using UnityEngine;

public class WallHighlight_W : MonoBehaviour
{
    public Material normalMaterial;  
    public Material highlightMaterial; 
    private Renderer wallRenderer;  
    public bool canInteract = false;

    void Start()
    {
        wallRenderer = GetComponent<Renderer>();
        wallRenderer.material = normalMaterial;
    }

    // Call this when the boss is defeated
    public void ActivateWall()
    {
        wallRenderer.material = highlightMaterial;
        canInteract = true;
    }
    public void TurnBackColor()
    {
        wallRenderer.material = normalMaterial;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (canInteract && other.CompareTag("Player"))
        {
            Debug.Log("Press E to start drawing.");
        }
    }

    public bool CanInteract()
    {
        return canInteract;
    }
}
