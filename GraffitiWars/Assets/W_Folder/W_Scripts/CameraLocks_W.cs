using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraLocks_W : MonoBehaviour
{
    public GameObject Main;
    public bool MainCamOn = true;
    public GameObject UITrig;

    [Header("Part1------------------------------------------------------------Part1")]
    public GameObject Part1Cam;
    public GameObject WaveSpawn1;
    public bool P1Activated = false;
    public GameObject Wall1;
    public GameObject Wall2;

    [Header("Part2------------------------------------------------------------Part2")]
    public GameObject Part2Cam;
    public GameObject WaveSpawn2;
    public bool P2Activated = false;
    public GameObject Wall3;
    public GameObject Wall4;

    [Header("Part3------------------------------------------------------------Part3")]
    public GameObject Part3Cam;
    public GameObject WaveSpawn3;
    public bool P3Activated = false;
    public GameObject Wall5;
    public GameObject Wall6;
    [Header("Part4------------------------------------------------------------Part4")]
    public GameObject BossCam;
    public bool PBossActivated = false;
    public GameObject Wall7;
    public GameObject Wall8;
    private void Awake()
    {
        Main.SetActive(true);
        Part1Cam.SetActive(false);
        Part2Cam.SetActive(false);
        Part3Cam.SetActive(false);
        BossCam.SetActive(false);
        WaveSpawn1.SetActive(false);
        WaveSpawn2.SetActive(false);
        WaveSpawn3.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleEvent(string e)
    {
        if(e == "Part1")
        {
            Main.SetActive(false);
            Part1Cam.SetActive(true);
            WaveSpawn1.SetActive(true);
            Wall1.SetActive(true);
            Wall2.SetActive(true);
        }
        else if(e == "Part1End")
        {
            Part1Cam.SetActive(false);
            WaveSpawn1.SetActive(false);
            Main.SetActive(true);
            Wall2.SetActive(false);
            UITrig.SetActive(true);
        }
        else if (e == "Part2")
        {
            UITrig.SetActive(false);
            Main.SetActive(false);
            Part2Cam.SetActive(true);
            WaveSpawn2.SetActive(true);
            Wall3.SetActive(true);
            Wall4.SetActive(true);
        }
        else if (e == "Part2End")
        {
            Part2Cam.SetActive(false);
            WaveSpawn2.SetActive(false);
            Main.SetActive(true);
            Wall4.SetActive(false);
            UITrig.SetActive(true);
        }
        else if (e == "Part3")
        {
            UITrig.SetActive(false);
            Main.SetActive(false);
            Part3Cam.SetActive(true);
            WaveSpawn3.SetActive(true);
            Wall5.SetActive(true);
            Wall6.SetActive(true);
        }
        else if (e == "Part3End")
        {
            Part3Cam.SetActive(false);
            WaveSpawn3.SetActive(false);
            Main.SetActive(true);
            Wall6.SetActive(false);
            UITrig.SetActive(true);
        }
        else if (e == "BossPart")
        {
            UITrig.SetActive(false);
            Main.SetActive(false);
            BossCam.SetActive(true);
            Wall7.SetActive(true);
            Wall8.SetActive(true);

        }
    }

}
