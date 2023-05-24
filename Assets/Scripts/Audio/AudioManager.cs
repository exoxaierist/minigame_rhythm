using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public BeatSystem beatSys;
    GameObject sceneChanger;
    private void Awake()
    {
        sceneChanger = GameObject.Find("SceneManager");
        beatSys.songName = sceneChanger.GetComponent<SceneChanger>().musicName;
    }
    
}
