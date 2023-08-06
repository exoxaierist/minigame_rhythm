using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicList : MonoBehaviour
{
    public static MusicList instance;
    public AudioClip[] BGM;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
        
        DontDestroyOnLoad(this.gameObject);
    }
}
