using UnityEngine;
using UnityEngine.UI;

public class ApplyGraffiti_W : MonoBehaviour
{
    public GraffitiDrawer_W graffitiDrawer;
    public Renderer wallRenderer;
    
    WallInteraction_W ui;

    void Start()
    {
        ui = GetComponent<WallInteraction_W>(); 
    }

    public void OnDoneButtonClicked()
    {
        Texture2D finalDrawing = graffitiDrawer.GetDrawing();

        ApplyDrawingToWall(finalDrawing);

        ui.CloseDrawingUI();
    }

    private void ApplyDrawingToWall(Texture2D drawing)
    {
        wallRenderer.material.mainTexture = drawing;
    }
}
