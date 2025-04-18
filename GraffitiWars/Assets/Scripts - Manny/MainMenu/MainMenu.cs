using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
