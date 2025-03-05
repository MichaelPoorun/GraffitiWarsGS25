using UnityEngine;

public class CameraLocks_W : MonoBehaviour
{
    public GameObject Main;
    public bool MainCamOn = true;
    
    [Header("Part1--------------------------------------------------")]
    public GameObject Part1Cam;
    public bool P1Activated = false;
    public GameObject Wall1;
    public GameObject Wall2;

    [Header("Part2--------------------------------------------------")]
    public GameObject Part2Cam;
    public bool P2Activated = false;
    public GameObject Wall3;
    public GameObject Wall4;

    [Header("Part3--------------------------------------------------")]
    public GameObject Part3Cam;
    public bool P3Activated = false;
    public GameObject Wall5;
    public GameObject Wall6;

    private void Awake()
    {
        Main.SetActive(true);
        Part1Cam.SetActive(false);
        Part2Cam.SetActive(false);
        Part3Cam.SetActive(false);

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Wall2.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Wall4.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Wall6.SetActive(false);
        }
    }

    public void HandleEvent(string e)
    {
        if(e == "Part1")
        {
            Main.SetActive(false);
            Part1Cam.SetActive(true);
            Wall1.SetActive(true);
            Wall2.SetActive(true);
        }
        else if (e == "MainCamOn1")
        {
            Part1Cam.SetActive(false);
            Main.SetActive(true);
        }
        else if (e == "Part2")
        {
            Main.SetActive(false);
            Part2Cam.SetActive(true);
            Wall3.SetActive(true);
            Wall4.SetActive(true);
        }
        else if (e == "MainCamOn2")
        {
            Part2Cam.SetActive(false);
            Main.SetActive(true);
        }
        else if (e == "Part3")
        {
            Main.SetActive(false);
            Part3Cam.SetActive(true);
            Wall5.SetActive(true);
            Wall6.SetActive(true);
        }
        else if (e == "MainCamOn3")
        {
            Part2Cam.SetActive(false);
            Main.SetActive(true);
        }
    }

}
