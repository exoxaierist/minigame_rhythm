using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;
    public GameObject[] player = new GameObject[2];
    public int[] score = new int[2];
    Vector3[] startingPos = new Vector3[2];
    ScoreUI scoreUI;
    private void Awake()
    {
        RoundManager.instance = this;
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
            StartCoroutine(Delay());
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
        if (BeatSystem.instance.gameStart)
        {
            BeatSystem.instance.Stop();
            yield return new WaitForSeconds(3f);         
            Reset();
            Global.OnCounterEnd?.Invoke();
        }
        else Debug.Log("게임이 시작되지 않았습니다.");
    }

}

