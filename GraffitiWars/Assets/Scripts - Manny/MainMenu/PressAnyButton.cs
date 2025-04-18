using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyButton : MonoBehaviour
{
    private bool hasPressedKey = false;
    [SerializeField] private string menuSceneName = "MainMenu";

    void Update()
    {
        if (!hasPressedKey && Input.anyKeyDown)
        {
            hasPressedKey = true;
            LoadMenu();
        }
    }

    void LoadMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}
