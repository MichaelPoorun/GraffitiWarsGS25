using UnityEngine;

public class CameraLocks_W : MonoBehaviour
{
    public GameObject Main;
    public GameObject Part1;
    public GameObject Part1Cam;
    public GameObject Part2;
    public GameObject Part2Cam;

    public bool MainCamOn = true;
    public bool Part1On = false;
    public bool Part2On = false;

    private void Awake()
    {
        Main.SetActive(true);
        Part1Cam.SetActive(false);
        Part2Cam.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(MainCamOn == false)
        {
            Main.SetActive(false);
            Part1Cam.SetActive(true);
            Debug.Log("CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC");
        }
    }

    public void HandleEvent(string e)
    {
        if(e == "Event1")
        {
            MainCamOn = false;
            Part1On = true;
            Debug.Log("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB");
        }
        else if (e == "Event2")
        {

        }
    }

    /*
    public void OnTriggerEnter(Collision other)
    {
        if (other.Part1.tag == "Player")
        {
            MainCamOn = false;
            Part1On = true;
        }
    }
    */
}
