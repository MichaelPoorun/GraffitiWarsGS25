using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NEW_CameraLock : MonoBehaviour
{
    public WaveManager waveManager;

    public GameObject Main;
    public bool MainCamOn = true;

    [Header("Part1----")]
    public GameObject Part1Cam;
    public GameObject WaveSpawn1;
    public GameObject[] Part1SpawnPoints;
    public GameObject Wall1;
    public GameObject Wall2;

    [Header("Part2----")]
    public GameObject Part2Cam;
    public GameObject WaveSpawn2;
    public GameObject[] Part2SpawnPoints;
    public GameObject Wall3;
    public GameObject Wall4;

    [Header("Part3----")]
    public GameObject Part3Cam;
    public GameObject WaveSpawn3;
    public GameObject[] Part3SpawnPoints;
    public GameObject Wall5;
    public GameObject Wall6;

    private void Awake()
    {
        Main.SetActive(true);
        Part1Cam.SetActive(false);
        Part2Cam.SetActive(false);
        Part3Cam.SetActive(false);
        WaveSpawn1.SetActive(false);
        WaveSpawn2.SetActive(false);
        WaveSpawn3.SetActive(false);
    }

    public void HandleEvent(string e)
    {
        if (e == "Part1")
        {
            Main.SetActive(false);
            Part1Cam.SetActive(true);
            WaveSpawn1.SetActive(true);
            Wall1.SetActive(true);
            Wall2.SetActive(true);

            waveManager.SetSpawnPointsForSection(0, Part1SpawnPoints);
        }
        else if (e == "Part2")
        {
            Main.SetActive(false);
            Part2Cam.SetActive(true);
            WaveSpawn2.SetActive(true);
            Wall3.SetActive(true);
            Wall4.SetActive(true);

            waveManager.SetSpawnPointsForSection(1, Part2SpawnPoints);
        }
        else if (e == "Part3")
        {
            Main.SetActive(false);
            Part3Cam.SetActive(true);
            WaveSpawn3.SetActive(true);
            Wall5.SetActive(true);
            Wall6.SetActive(true);

            waveManager.SetSpawnPointsForSection(2, Part3SpawnPoints);
        }
    }

    public void UnlockBarrier(GameObject barrier)
    {
        barrier.SetActive(false);
    }
}
