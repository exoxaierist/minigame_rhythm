using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BeatSystem : MonoBehaviour
{
    public static BeatSystem instance;
    [Header("���� ��")] public string songName = "bgm2";
    SongManager sM;
    MidiFileParser mP;
    NoteManager nM;
    public bool gameStart = false;

    //
    Transform detectionLine;

    private void Awake()
    {
        instance = this;
        detectionLine = transform.Find("UI").Find("DetectionLine");
        sM = GetComponent<SongManager>();
        mP = GetComponent<MidiFileParser>();
        nM = GetComponent<NoteManager>();
        if (GameObject.Find("SceneManager") != null)
        {
            songName = GameObject.Find("SceneManager").GetComponent<SceneChanger>().musicName;
            if (sM.SearchSong(songName) == null)
                mP.GetMidiFile(sM.BGM[1].name);
        }
        else
            mP.GetMidiFile(sM.BGM[1].name);
        
        Global.GetTimingms += GetDelaytime;
        
        Global.CheckBeat += Detection;
        
        Global.OnCounterEnd += Play;

    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            DOTween.Rewind(detectionLine);
            DOTween.Rewind(detLineIma);
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
        }*/

        if (gameStart && !nM.isPlaying) Play();
    }

    /** ���ǰ� ��Ʈ ��� */
    public void Play()
    {
        if (nM.isPlaying) Stop();
        if (!gameStart) gameStart = true;
        mP.GetMidiFile(songName);
        sM.canPlay = true;
        sM.Invoke("PlaySong", nM.notePlayingTime);
        nM.noteInfo = mP.GetDataFromMidi();
        StartCoroutine(nM.PlayNote());
    }

    public void Stop()
    {
        gameStart = false;
        sM.audioSource.Stop();
        sM.canPlay = false;
        nM.ResetNote();
    }

    public float GetDelaytime()
    {
        if (!nM.isPlaying) return 0;
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
        if (!nM.isPlaying || !sM.audioSource.isPlaying)
        {
            return false;
            Debug.Log("����");
        }
        if (GetDelaytime() <= 0.15f)
            return true;

        return false;
    }


}
