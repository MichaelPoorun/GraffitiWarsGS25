using UnityEngine;

public class WallHighlight_W : MonoBehaviour
{
    public Material normalMaterial;  
    public Material highlightMaterial; 
    public Renderer wallRenderer;  
    public bool canInteract = false; //Makes Wall Interactable (Plyaer Can Click E On It)
    public GameObject BossAlive;
    public bool BA = false;
    public GameObject SprayOnWallUI;

    void Update()
    {
        if (BossAlive == null && BA == false)
        {
            HighlightWall();
        }
    }

    void Start()
    {
        wallRenderer = GetComponent<Renderer>();
        wallRenderer.material = normalMaterial;
    }

    // Call this when the boss is defeated
    public void HighlightWall()
    {
        wallRenderer.material = highlightMaterial;
        SprayOnWallUI.SetActive(true);
    }
    public void TurnBackColor()
    {
        wallRenderer.material = normalMaterial;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (canInteract && other.CompareTag("Player"))
        {
            //UI Saying This vvv
            Debug.Log("Press E to start drawing.");
        }
    }

    public bool CanInteract()
    {
        return canInteract;
    }
}
