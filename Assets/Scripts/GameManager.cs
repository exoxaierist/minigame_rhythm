using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Reflection;

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

    public void ChangeScene(int mapNumber)
    {
        if (!Application.CanStreamedLevelBeLoaded(mapNumber)) return;
        SceneManager.LoadScene(mapNumber);
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
            Debug.Log($"Variable Name: {field.Name}");
            if (field.FieldType == typeof(int) || field.FieldType == typeof(float) || field.FieldType == typeof(LayerMask) || field.FieldType == typeof(Vector2)) 
                continue;
            field.SetValue(null, null);
        }

        RoundManager.instance = null;
        BeatSystem.instance = null;
        GameManager.instance = null;
    }
}
