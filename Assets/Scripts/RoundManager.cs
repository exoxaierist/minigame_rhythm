using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RoundManager : MonoBehaviour
{
    public int maxScore = 2;
    public static RoundManager instance;
    public GameObject[] player = new GameObject[2];
    public int[] score = new int[2];
    Vector3[] startingPos = new Vector3[2];
    ScoreUI scoreUI;
    private void Awake()
    {
        if(RoundManager.instance == null) RoundManager.instance = this;
        if(GameObject.Find("UI").transform.Find("ScoreUI").gameObject.GetComponent<ScoreUI>() != null)
            scoreUI = GameObject.Find("UI").transform.Find("ScoreUI").gameObject.GetComponent<ScoreUI>();
        var obj = FindObjectsOfType<RoundManager>();
        for (int i = 0; i < 2; i++)
        {
            int playerIndex = i;
            player[i] = GameObject.Find("P" + (i + 1).ToString());
            player[i].GetComponent<Hp>().OnDeath += () => Win(1 - playerIndex);
            startingPos[i] = player[i].transform.position;
        }
    }
  
    public void Win(int player)
    {
        if(BeatSystem.instance.gameStart)
        {
            score[player] += 1;            
            if (scoreUI != null) scoreUI.ShowUI(player);
            BeatSystem.instance.Stop();
            if (score[player] != maxScore) StartCoroutine(Delay());
            else StartCoroutine(ExitGame());
        }       
    }

    public void Reset()
    {
        for (int i = 0; i < 2; i++)
        {
            player[i].transform.position = startingPos[i];
            player[i].GetComponent<Hp>().AddToHP(10);
            Global.energyManager.ResetP1Energy();
            Global.energyManager.ResetP2Energy();
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        Reset();
        GameObject.Find("UI").transform.Find("GameStartCounter").GetComponent<StartCounter>().Initialize();
    }

    IEnumerator ExitGame()
    {
        yield return new WaitForSeconds(3);
        GameManager.instance.ChangeScene(0);
        GameManager.instance.UnloadCurrentScene();       
    }

}

