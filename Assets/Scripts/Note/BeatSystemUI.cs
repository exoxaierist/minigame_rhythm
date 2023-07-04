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
        BeatUI.GetComponent<Image>().DOFade(0f, 0);
        speaker[0].GetComponent<Image>().DOFade(0f, 0);
        speaker[1].GetComponent<Image>().DOFade(0f, 0);
    }


    void PumpSpeaker()
    {
        speaker[0].transform.DOComplete();
        speaker[1].transform.DOComplete();
        speaker[0].transform.DOShakeScale(0.3f, 0.5f, 15);
        speaker[1].transform.DOShakeScale(0.3f, 0.5f, 15);
    }


    void AppearUI()
    {        
        BeatUI.transform.position = BeatUI.transform.position - new Vector3(0, -100, 0);
        BeatUI.transform.DOMove(BeatUI.transform.position + new Vector3(0, -100, 0), 0.5f).SetEase(Ease.OutCubic);
        BeatUI.GetComponent<Image>().DOFade(1, 0.1f);
        speaker[0].GetComponent<Image>().DOFade(1, 0.1f);
        speaker[1].GetComponent<Image>().DOFade(1, 0.1f);
        speaker[0].transform.position += new Vector3(0,100,0);
        speaker[1].transform.position += new Vector3(0,100,0);
        speaker[0].transform.DOLocalMoveY(0, 0.5f).SetEase(Ease.OutCubic).SetDelay(0.3f);
        speaker[1].transform.DOLocalMoveY(0, 0.5f).SetEase(Ease.OutCubic).SetDelay(0.3f);
        BeatUI.SetActive(true);
    }

}
