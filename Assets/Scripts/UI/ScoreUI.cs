using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class ScoreUI : MonoBehaviour
{
    public GameObject[] scoreText = new GameObject[2];
    TextMeshProUGUI winnerText = new TextMeshProUGUI();

    private void Awake()
    {
        winnerText = transform.Find("UI").Find("winner").GetComponent<TextMeshProUGUI>();
    }

    public void ShowUI(int player = -1)
    {
        if(!scoreText[0].transform.parent.gameObject.activeSelf && RoundManager.instance.maxScore != RoundManager.instance.score[player]) Invoke("HideUIEffect", 2f);
        scoreText[0].transform.parent.gameObject.SetActive(true);      
        if(player != -1)
        {
            if(RoundManager.instance.score[player] != RoundManager.instance.maxScore) winnerText.text = "P"+(player+1)+" Win!";
            else winnerText.text = "P" + (player + 1) + " Win!\nCongratulation!";
            scoreText[player].transform.DOMoveY(scoreText[player].transform.position.y + 50, 0.2f).OnComplete(() =>
            {
                scoreText[player].transform.DOMoveY(scoreText[player].transform.position.y - 50, 0.2f);
                scoreText[player].GetComponent<TMP_Text>().text = RoundManager.instance.score[player].ToString();
            });          
        }            
    }

    public void HideUIEffect()
    {
        transform.Find("background").GetComponent<Image>().DOFade(1.5f, 1).OnComplete(() =>
        {
            HideUI();
            transform.Find("background").GetComponent<Image>().DOFade(0, 0.5f);
        });      
    }

    public void HideUI()
    {
        scoreText[0].transform.parent.gameObject.SetActive(false);
    }
}
