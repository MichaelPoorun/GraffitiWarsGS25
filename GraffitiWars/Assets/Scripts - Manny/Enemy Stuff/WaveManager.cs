using UnityEngine;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public WaveSpawner waveSpawner;
    public GameObject[] sections; // areas of the level to unlock
    public GameObject[] barriers;
    public Transform[][] sectionSpawnPoints;

    private int currentSectionIndex = 0;

    void Start()
    {
        sectionSpawnPoints = new Transform[sections.Length][];
        ActivateSection(currentSectionIndex);
        ToggleBarrier(currentSectionIndex, true);
    }

    public void OnWaveCompleted()
    {
        /*if (currentSectionIndex < sections.Length - 1)
        {
            ToggleBarrier(currentSectionIndex, false);
            currentSectionIndex++;
            ActivateSection(currentSectionIndex);
            ToggleBarrier(currentSectionIndex, true);
            waveSpawner.currWave++;
            waveSpawner.GenerateWave();
        }
        else
        {
            Debug.Log("All sections cleared!");
            ToggleBarrier(currentSectionIndex, false);
        }*/
    }

    private void ActivateSection(int index)
    {
        for (int i = 0; i < sections.Length; i++)
        {
            sections[i].SetActive(i == index);
        }
    }

    private void ToggleBarrier(int index, bool activate)
    {
        if (index < barriers.Length)
        {
            barriers[index].SetActive(activate);
        }
    }

    public void SetSpawnPointsForSection(int index, GameObject[] spawnPoints)
    {
        Transform[] spawnPointTransforms = new Transform[spawnPoints.Length];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPointTransforms[i] = spawnPoints[i].transform;
        }

        sectionSpawnPoints[index] = spawnPointTransforms;
        waveSpawner.spawnLocations = new List<Transform>(sectionSpawnPoints[index]);
    }
}
