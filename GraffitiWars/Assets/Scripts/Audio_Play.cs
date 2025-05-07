using UnityEngine;

public class Audio_Play : MonoBehaviour
{

    AudioSource AS;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play_sound()
    {
        AS.Play();
    }
}
