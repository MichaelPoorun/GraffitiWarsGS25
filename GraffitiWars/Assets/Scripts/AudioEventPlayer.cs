using UnityEngine;
using System.Collections.Generic;

public class AudioEventPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public List<NamedAudioClip> audioClips = new List<NamedAudioClip>();

    [System.Serializable]
    public class NamedAudioClip
    {
        public string name;
        public AudioClip clip;
    }

    private Dictionary<string, AudioClip> clipDict;

    private void Awake()
    {
        clipDict = new Dictionary<string, AudioClip>();
        foreach (var entry in audioClips)
        {
            if (!clipDict.ContainsKey(entry.name))
            {
                clipDict.Add(entry.name, entry.clip);
            }
        }
    }

    // Called from Animation Event
    public void PlaySound(string clipName)
    {
        if (clipDict != null && clipDict.TryGetValue(clipName, out var clip))
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"Audio clip '{clipName}' not found in {gameObject.name}", this);
        }
    }
}
