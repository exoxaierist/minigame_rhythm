using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class ScoreUI : MonoBehaviour
{
    public GameObject[] scoreText = new GameObject[2];

    public void ShowUI(int player = -1)
    {
        if(!scoreText[0].transform.parent.gameObject.activeSelf) Invoke("HideUI", 2f);
        scoreText[0].transform.parent.gameObject.SetActive(true);      
        if(player != -1)
        {
            scoreText[player].transform.DOMoveY(scoreText[player].transform.position.y + 50, 0.2f).OnComplete(() =>
            {
                scoreText[player].transform.DOMoveY(scoreText[player].transform.position.y - 50, 0.2f);
                scoreText[player].GetComponent<TMP_Text>().text = RoundManager.instance.score[player].ToString();
            });
        }            
    }

    public void HideUI()
    {
        scoreText[0].transform.parent.gameObject.SetActive(false);
    }
}
