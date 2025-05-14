using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public RawImage graffiti;

    private void Start()
    {
        if (GraffitiData.SavedTexture != null)
        {
            graffiti.texture = GraffitiData.SavedTexture;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("W_Blockout");
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenMoveList()
    {
        SceneManager.LoadScene("Movelist");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
