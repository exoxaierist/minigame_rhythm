using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BeatSystem : MonoBehaviour
{
    [Header("���� ��")] public string songName = "bgm1";
    SongManager sM;
    MidiFileParser mP;
    NoteManager nM;

    //
    Transform detectionLine;
    Image detLineIma;

    private void Awake()
    {
        Global.GetTimingms = GetDelaytime;
        Global.CheckBeat = Detection;
    }

    private void Start()
    {
        Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DOTween.Rewind(detectionLine);
            DOTween.Rewind(detLineIma);
            Debug.Log(GetDelaytime());
            if (Detection())
            {
                detectionLine.DOScale(detectionLine.localScale * 1.1f, 0.2f).SetLoops(2, LoopType.Yoyo);
                detLineIma.DOColor(Color.green, 0.25f).SetLoops(2, LoopType.Yoyo);
            }
            else
            {
                detectionLine.DOScale(detectionLine.localScale * 1.1f, 0.2f).SetLoops(2, LoopType.Yoyo);
                detLineIma.DOColor(Color.red, 0.25f).SetLoops(2, LoopType.Yoyo);
            }
        }

        
        if (!nM.isPlaying && !sM.audioSource.isPlaying)
        {
            Debug.Log("���� �����");
            Play();
        }
        
    }

    /** ���ǰ� ��Ʈ ��� */
    public void Play()
    {
        detectionLine = transform.Find("UI").Find("DetectionLine");
        detLineIma = detectionLine.GetComponent<Image>();
        sM = GetComponent<SongManager>();
        mP = GetComponent<MidiFileParser>();
        nM = GetComponent<NoteManager>();
        mP.GetMidiFile(songName);
        sM.Invoke("PlaySong", nM.notePlayingTime);
        nM.noteInfo = mP.GetDataFromMidi();
        StartCoroutine(nM.PlayNote());
    }


    public float GetDelaytime()
    {
        float t1 = 0, t2 = 0;
        if (nM.hitCount != nM.noteInfo.Count) t1 = Mathf.Abs(nM.noteInfo[nM.hitCount].hitTime + nM.correctionValue - sM.audioSource.time);
        else t1 = Mathf.Abs(nM.noteInfo[nM.hitCount-1].hitTime + nM.correctionValue - sM.audioSource.time);
        if (nM.hitCount != 0)
        {
            t2 = Mathf.Abs(nM.noteInfo[nM.hitCount - 1].hitTime + nM.correctionValue - sM.audioSource.time);
            if (t2 < t1) return t2;
        }
        return t1;
    }

    /** ��Ʈ ���� ��� */
    public bool Detection()
    {
        if (GetDelaytime() <= 0.13f)
            return true;

        return false;
    }

}