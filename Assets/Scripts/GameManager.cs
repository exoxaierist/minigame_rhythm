using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Reflection;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        Transform menu = GameObject.Find("UI").transform.Find("Menu");
        menu.Find("contunue").GetComponent<Button>().onClick.AddListener(ContinueGame);
        menu.Find("main").GetComponent<Button>().onClick.AddListener(() => ChangeScene(0));
        menu.Find("main").GetComponent<Button>().onClick.AddListener(UnloadCurrentScene);

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

    public void ChangeScene(int mapNumber)
    {
        SetTimeScale(1);
        if (!Application.CanStreamedLevelBeLoaded(mapNumber)) return;
        SceneManager.LoadScene(mapNumber);
        SetSound(false);
    }

    public void UnloadCurrentScene()
    {
        UnloadStaticValues();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.UnloadSceneAsync(currentSceneIndex);
    }

    public void SetTimeScale(float value)
    {
        Time.timeScale = value;
    }

    public void SetSound(bool value)
    {
        AudioListener.pause = value;
    }

    public void ContinueGame() 
    {
        SetSound(false);
        SetTimeScale(1);
    }

    public void UnloadStaticValues()
    {
        FieldInfo[] staticFields = typeof(Global).GetFields(BindingFlags.Public | BindingFlags.Static);

        foreach (var field in staticFields)
        {
            if (field.FieldType == typeof(int) || field.FieldType == typeof(float) || field.FieldType == typeof(LayerMask) || field.FieldType == typeof(Vector2) || field.FieldType == typeof(Color)) 
                continue;
            field.SetValue(null, null);
        }

        RoundManager.instance = null;
        BeatSystem.instance = null;
        GameManager.instance = null;
    }
}
