using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public WaveSpawner waveSpawner;
    public GameObject[] sections;
    private int currentSectionIndex = 0;

    void Start()
    {
        ActivateSection(currentSectionIndex);
    }

    public void OnWaveCompleted()
    {
        if (currentSectionIndex < sections.Length - 1)
        {
            currentSectionIndex++;
            ActivateSection(currentSectionIndex);
            waveSpawner.currWave++;
            waveSpawner.GenerateWave();
        }
        else
        {
            Debug.Log("All sections cleared!");
        }
    }

    private void ActivateSection(int index)
    {
        for (int i = 0; i < sections.Length; i++)
        {
            sections[i].SetActive(i == index);
        }
    }
}
