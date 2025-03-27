using System.Drawing;
using UnityEngine;

public class WallInteraction_W : MonoBehaviour
{
    public GameObject drawingUI;
    private WallHighlight_W wallHighlight;
    
    void Start()
    {
        wallHighlight = GetComponent<WallHighlight_W>();
    }

    void OnTriggerStay(Collider other)
    {
        if (wallHighlight.CanInteract() && Input.GetKeyDown(KeyCode.E))
        {
            wallHighlight.TurnBackColor();
            OpenDrawingUI();
        }
    }

    void OpenDrawingUI()
    {
        drawingUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseDrawingUI()
    {
        drawingUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
