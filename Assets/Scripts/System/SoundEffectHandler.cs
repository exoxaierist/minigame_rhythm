using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectHandler : MonoBehaviour
{
    public GameObject sfxInstance;
    private Transform sfxParent;
    private List<SoundEffect> pool = new();

    private void Awake()
    {
        Global.sfx = this;
        sfxParent = new GameObject().transform;
        sfxParent.name = "sfx";
        sfxParent.SetParent(transform);
    }

    public void Play(AudioClip clip)
    {
        Get().Play(clip);
    }

    private SoundEffect Get()
    {
        foreach (SoundEffect sfx in pool) if (sfx.isFree) return sfx;
        return Create();
    }

    private SoundEffect Create()
    {
        SoundEffect instance = Instantiate(sfxInstance, sfxParent).GetComponent<SoundEffect>();
        pool.Add(instance);
        return instance;
    }
}
