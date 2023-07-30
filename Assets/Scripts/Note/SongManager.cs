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
        audioSource.outputAudioMixerGroup.audioMixer.SetFloat("BGM", 0);
        if (SearchSong(GetComponent<BeatSystem>().songName) != null)
            audioSource.clip = SearchSong(GetComponent<BeatSystem>().songName);
        else
            audioSource.clip = null;

        if(canPlay) audioSource.Play();
    }

    public AudioClip SearchSong(string name)
    {
        BGM = GameObject.Find("MusicList").GetComponent<MusicList>().BGM;
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

    public void setVolume(int volume, float duration = 0f)
    {
        if(volume <= 0) StartCoroutine(setVol(volume, duration));
    }

    public IEnumerator setVol(int volume, float duration)
    {        
        if (duration != 0f)
        {
            for (int i = 0; i < duration / 0.1f; i++)
            {
                audioSource.outputAudioMixerGroup.audioMixer.SetFloat("BGM", volume / (duration / 0.1f) * i);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else audioSource.outputAudioMixerGroup.audioMixer.SetFloat("BGM", volume);
    }
}
