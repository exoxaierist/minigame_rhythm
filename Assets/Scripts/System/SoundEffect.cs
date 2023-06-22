using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public bool isFree = true;
    private AudioSource audio;

    public void Play(AudioClip clip)
    {
        if (!isFree) return;
        gameObject.SetActive(true);
        isFree = false;
        if (audio == null) audio = GetComponent<AudioSource>();
        audio.clip = clip;
        audio.Play();
        StartCoroutine(Timer(clip.length));
    }

    private void Stop()
    {
        audio.Stop();
        isFree = true;
        gameObject.SetActive(false);
    }

    private IEnumerator Timer(float t)
    {
        yield return new WaitForSecondsRealtime(t);
        Stop();
    }
}
