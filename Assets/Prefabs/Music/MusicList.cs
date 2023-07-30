using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicList : MonoBehaviour
{
    public AudioClip[] BGM;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
