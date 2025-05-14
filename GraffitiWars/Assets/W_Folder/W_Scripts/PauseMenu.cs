using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public static bool isPaused;
    public GameObject q;
    public GameObject w;
    public GameObject e;
    public GameObject r;
    public GameObject t;
    public GameObject button;
    public GameObject quitbutton;

    public void Awake()
    {
        q.SetActive(false);
        w.SetActive(false);
        e.SetActive(false);
        r.SetActive(false);
        t.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void OpenMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainStart_W");
    }


    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainGame_W");
    }

    public void OpenMovelist()
    {
        q.SetActive(true);
        w.SetActive(true);
        e.SetActive(true);
        r.SetActive(true);
        t.SetActive(true);
        button.SetActive(true);
        quitbutton.SetActive(false);
    }
    public void CloseMovelist()
    {
        q.SetActive(false);
        w.SetActive(false);
        e.SetActive(false);
        r.SetActive(false);
        t.SetActive(false);
        button.SetActive(false);
        quitbutton.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
