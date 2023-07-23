using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string musicName;
    public int MapNum;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
 
    public void GameStart()
    {
        SceneManager.LoadScene(MapNum);
    }
}
