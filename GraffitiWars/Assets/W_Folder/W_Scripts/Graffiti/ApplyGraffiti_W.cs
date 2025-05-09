using UnityEngine;
using UnityEngine.UI;

public class ApplyGraffiti_W : MonoBehaviour
{
    public GraffitiDrawer_W graffitiDrawer;
    public Renderer wallRenderer;
    
    WallInteraction_W ui;

    public GameObject target;
    public CameraLocks_W cams;
    public GameObject GO;

    bool DonePressed = false;

    private void Update()
    {
        if (DonePressed == true)
        {
            cams.BossCam.SetActive(false);
            cams.Wall8.SetActive(false);
            GO.SetActive(true);
            DonePressed = false;
        }
    }
    void Start()
    {
        ui = GetComponent<WallInteraction_W>();
        cams = GetComponent<CameraLocks_W>();
        cams = target.GetComponent<CameraLocks_W>();
    }

    public void OnDoneButtonClicked()
    {
        Texture2D finalDrawing = graffitiDrawer.GetDrawing();

        ApplyDrawingToWall(finalDrawing);

        DonePressed = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        ui.CloseDrawingUI();

        CanvasRenderer virtualCursorRenderer = graffitiDrawer.virtualCursor.GetComponent<CanvasRenderer>();
        virtualCursorRenderer.SetAlpha(0f);
    }

    private void ApplyDrawingToWall(Texture2D drawing)
    {
        wallRenderer.material.mainTexture = drawing;
    }
}
