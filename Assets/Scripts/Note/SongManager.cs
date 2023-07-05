using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    public AudioClip[] BGM;
    public AudioSource audioSource;
    public bool canPlay = false;

    public void PlaySong()
    {
        if (SearchSong(GetComponent<BeatSystem>().songName) != null)
            audioSource.clip = SearchSong(GetComponent<BeatSystem>().songName);
        else
            audioSource.clip = null;

        if(canPlay) audioSource.Play();
    }

    public AudioClip SearchSong(string name)
    {
        foreach(AudioClip clip in BGM)
        {
            if(clip.name == name)
            {
                if (audioSource == null)
                    audioSource = GetComponent<AudioSource>();
                return clip;
            }
        }

        return null;
    }

}
