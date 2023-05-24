using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BeatSystemUI : MonoBehaviour
{
    public GameObject BeatUI;
    GameObject[] speaker = new GameObject[2];
    Vector3 size;
    bool isPumping = false;

    private void Awake()
    {
        BeatUI = transform.Find("UI").gameObject;
        BeatUI.SetActive(false);
        speaker[0] = BeatUI.transform.Find("Speaker1").gameObject;
        speaker[1] = BeatUI.transform.Find("Speaker2").gameObject;
        size = speaker[0].transform.localScale;
        Global.OnBeat += PumpSpeaker;
        Global.OnCounterEnd += AppearUI;
    }


    void PumpSpeaker()
    {
        if(!isPumping)
        {
            isPumping = true;
            speaker[0].transform.DOScale(speaker[0].transform.localScale * 1.3f, 0.08f).OnComplete(() =>
                speaker[0].transform.DOScale(size, 0.25f));
            speaker[1].transform.DOScale(speaker[1].transform.localScale * 1.3f, 0.08f).OnComplete(() =>
            speaker[1].transform.DOScale(size, 0.25f).OnComplete(() => isPumping = false));
        }      
    }


    void AppearUI()
    {       
        BeatUI.transform.position = BeatUI.transform.position - new Vector3(0, 100, 0);
        BeatUI.transform.DOMove(BeatUI.transform.position + new Vector3(0, 100, 0), 1.5f);
        BeatUI.GetComponent<Image>().DOFade(0f, 0);
        speaker[0].GetComponent<Image>().DOFade(0f, 0);
        speaker[1].GetComponent<Image>().DOFade(0f, 0);
        BeatUI.SetActive(true);
        BeatUI.GetComponent<Image>().DOFade(0.9f, 1);
        speaker[0].GetComponent<Image>().DOFade(1, 1);
        speaker[1].GetComponent<Image>().DOFade(1, 1);
    }
}
