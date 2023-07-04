using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;
    public Hp[] hp = new Hp[2];
    public int[] score = new int[2];
    ScoreUI scoreUI;
    private void Awake()
    {
        RoundManager.instance = this;
        if(GameObject.Find("UI").transform.Find("ScoreUI").gameObject.GetComponent<ScoreUI>() != null)
            scoreUI = GameObject.Find("UI").transform.Find("ScoreUI").gameObject.GetComponent<ScoreUI>();
        var obj = FindObjectsOfType<RoundManager>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {           
            Destroy(gameObject);
        }
        for (int i = 0; i < 2; i++)
        {
            int playerIndex = i;
            hp[i] = GameObject.Find("P" + (i + 1).ToString()).GetComponent<Hp>();
            hp[i].OnDeath += () => Win(1 - playerIndex);
        }
    }
  
    public void Win(int player)
    {
        score[player] += 1;
        if(scoreUI != null) scoreUI.ShowUI(player);
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        if (BeatSystem.instance.gameStart)
        {
            BeatSystem.instance.Stop();
            yield return new WaitForSeconds(3f);
            for (int i = 0; i < 2; i++) hp[i].HpReturn();
            Global.OnCounterEnd?.Invoke();
        }
        else Debug.Log("게임이 시작되지 않았습니다.");
    }

}

