using System.Drawing;
using UnityEngine;

public class WallInteraction_W : MonoBehaviour
{
    public GameObject drawingUI;
    private WallHighlight_W wallHighlight;
    
    void Start()
    {
        wallHighlight = GetComponent<WallHighlight_W>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnTriggerStay(Collider other)
    {
        if (wallHighlight.BossAlive == null && other.gameObject.CompareTag("Player"))
        {
            wallHighlight.canInteract = true;
            Debug.Log("Can Interact is set to true");
        }

        if (wallHighlight.CanInteract() && Input.GetButtonDown("Interact"))
        {
            wallHighlight.Arrow1.SetActive(false);
            wallHighlight.Arrow2.SetActive(false);
            wallHighlight.BA = true;
            wallHighlight.TurnBackColor();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
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
