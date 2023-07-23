using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && BeatSystem.instance.gameStart)
        {
            Time.timeScale = 0f;
            AudioListener.pause = true;
            GameObject.Find("UI").transform.Find("Menu").gameObject.SetActive(true);
        }
    }

    public void ChangeScene(string name)
    {
        if (!Application.CanStreamedLevelBeLoaded(name)) return;
        SceneManager.LoadScene(name);
    }

    public void SetTimeScale(float value)
    {
        Time.timeScale = value;
    }

    public void SetSound(bool value)
    {
        AudioListener.pause = value;
    }

    public void GameContinue() 
    {
        SetSound(false);
        SetTimeScale(1);
    }

}
