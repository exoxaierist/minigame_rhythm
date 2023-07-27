using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using DG.Tweening;

public class NoteManager : MonoBehaviour
{
    [Header("��Ʈ ��� �ð�")] public float notePlayingTime = 2f; // ��Ʈ�� �������� �ð�
    public GameObject note;
    public List<midiInfo> noteInfo = new List<midiInfo>();
    public int hitCount = 0; // ������ ��Ʈ ��
    public float correctionValue = 0; // ���� ��  
    //ȣ��� ����
    Transform noteIndex;
    Transform tempNote;
    Transform detectionLine;
    RectTransform noteUI;
    AudioSource bgm;
    //
    public bool isPlaying = false;
    void Awake()
    {
        bgm = GetComponent<AudioSource>();
        noteIndex = transform.Find("UI").Find("NoteIndex");
        detectionLine = transform.Find("UI").Find("DetectionLine");
        noteUI = transform.Find("UI").GetComponent<RectTransform>();
    }

    /** ��Ʈ ���(�ڷ�ƾ) */
    public IEnumerator PlayNote()
    {
        if (isPlaying) ResetNote();
        isPlaying = true;
        hitCount = 0;
        correctionValue = 0;
        float temp = 0;
        for (int count = 0; count < noteInfo.Count; count++)
        {
            yield return new WaitForSeconds(noteInfo[count].hitTime - temp - correctionValue / 10);
            if (!isPlaying)
                yield break;
            temp = noteInfo[count].hitTime;
            CreatNote();
        }
    }


    /** �Ϲ� ��Ʈ ����*/
    void CreatNote(int dir = 1)
    {
        tempNote = Instantiate(note, noteUI.anchoredPosition, Quaternion.identity).transform;
        tempNote.GetComponent<RectTransform>().anchoredPosition = new Vector2(dir * noteUI.rect.width/2, 0);
        tempNote.SetParent(noteIndex, false);
        tempNote.GetComponent<Image>().DOFade(1.0f, notePlayingTime);
        if (dir == 1)
        {
            tempNote.DOMoveX(detectionLine.position.x, notePlayingTime).SetEase(Ease.Linear).OnComplete(() => RemoveNote());
            CreatNote(-1);
        }
        else tempNote.DOMoveX(detectionLine.position.x, notePlayingTime).SetEase(Ease.Linear);      
    }

    public void ResetNote()
    {
        noteInfo = null;
        StopCoroutine(PlayNote());
        foreach (Transform child in noteIndex)
        {
            Destroy(child.gameObject);
        }
        isPlaying = false;
        hitCount = 0;
        Debug.Log("StopNote");
    }
    
    void RemoveNote()
    {
        correctionValue = bgm.time - noteInfo[hitCount].hitTime - 0.01f;
        Global.OnBeat?.Invoke();
        Destroy(noteIndex.GetChild(1).gameObject);
        Destroy(noteIndex.GetChild(0).gameObject);            
        if (noteInfo.Count > hitCount) hitCount++;
        if (noteInfo.Count == hitCount)
        {
            Debug.Log("���� ����");
            isPlaying = false;
            StopCoroutine(PlayNote());
            BeatSystem.instance.Play();
        }          
    }
}
