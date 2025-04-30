using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WallHighlight_W : MonoBehaviour
{
    public Material normalMaterial;  
    public Material highlightMaterial; 
    public Renderer wallRenderer;  
    public bool canInteract = false; //Makes Wall Interactable (Plyaer Can Click E On It)
    public GameObject BossAlive;
    public bool BA = false;
    public GameObject Arrow1;
    public GameObject Arrow2;
    public GameObject BossHealth;
    public CameraLocks_W cams;
    public GameObject target;

    void Update()
    {
        if (BossAlive == null && BA == false)
        {
            cams.Main.SetActive(true);
            HighlightWall();
            BossHealth.SetActive(false);
        }
    }

    void Start()
    {
        cams = GetComponent<CameraLocks_W>();
        cams = target.GetComponent<CameraLocks_W>();
        wallRenderer = GetComponent<Renderer>();
        wallRenderer.material = normalMaterial;
    }

    // Call this when the boss is defeated
    public void HighlightWall()
    {
        wallRenderer.material = highlightMaterial;
        Arrow1.SetActive(true);
        Arrow2.SetActive(true);
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
