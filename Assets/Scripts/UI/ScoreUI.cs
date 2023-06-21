using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class ScoreUI : MonoBehaviour
{
    public GameObject player;
    public GameObject scoreText;
    int score = 0;
    void Start()
    {
        player = GameObject.Find("P" + scoreText.name);
        player.GetComponent<Hp>().OnDeath += IncreaseScore;
    }

    void IncreaseScore()
    {
        if(!scoreText.transform.parent.gameObject.activeSelf) Invoke("HideUI", 2f);
        scoreText.transform.parent.gameObject.SetActive(true);
        score++;
        scoreText.transform.DOMoveY(scoreText.transform.position.y + 50, 0.2f).OnComplete(() =>
        {
            scoreText.transform.DOMoveY(scoreText.transform.position.y - 50, 0.2f);
            scoreText.GetComponent<TMP_Text>().text = score.ToString();          
        });        
    }

    void HideUI()
    {
        scoreText.transform.parent.gameObject.SetActive(false);
    }
}
