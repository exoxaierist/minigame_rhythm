using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BeatSystem : MonoBehaviour
{
    public static BeatSystem instance;
    [Header("현재 곡")] public string songName = "bgm2";
    SongManager sM;
    MidiFileParser mP;
    NoteManager nM;
    public bool gameStart = false;
    public bool CurrentBeatStatus = false;

    private void Awake()
    {
        instance = this;
        sM = GetComponent<SongManager>();
        mP = GetComponent<MidiFileParser>();
        nM = GetComponent<NoteManager>();
        GameObject tmp = GameObject.Find("SceneManager");
        if (tmp != null)
        {
            songName = tmp.GetComponent<SceneChanger>().musicName;
            if (sM.SearchSong(songName) == null)
                mP.GetMidiFile(sM.BGM[1].name);
        }
        else
            mP.GetMidiFile(sM.BGM[1].name);
        Destroy(tmp);
        Global.GetTimingms += GetDelaytime;
        
        Global.CheckBeat += Detection;
        
        Global.OnCounterEnd += Play;

    }

    private void Update()
    {
        if (CurrentBeatStatus && !Detection()) Global.OnLastTiming?.Invoke();
        CurrentBeatStatus = Detection();
    }

    /** 음악과 노트 재생 */
    public void Play()
    {
        sM.audioSource.Stop();
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
        sM.setVolume(-40, 5);
        nM.ResetNote();
    }

    public float GetDelaytime()
    {
        if (!nM.isPlaying) return 0;
        float t1 = 0, t2 = 0;
        if (nM.hitCount != nM.noteInfo.Count) t1 = Mathf.Abs(nM.noteInfo[nM.hitCount].hitTime - sM.audioSource.time);
        else t1 = Mathf.Abs(nM.noteInfo[nM.hitCount-1].hitTime  - sM.audioSource.time);
        if (nM.hitCount != 0)
        {
            t2 = Mathf.Abs(nM.noteInfo[nM.hitCount - 1].hitTime  - sM.audioSource.time);
            if (t2 < t1) return t2 - nM.correctionValue;
        }
        return t1 - nM.correctionValue;
    }

    /** 노트 판정 계산 */
    public bool Detection()
    {
        if (!nM.isPlaying || !sM.audioSource.isPlaying)
        {
            return false;
        }
        if (GetDelaytime() <= 0.15f)
            return true;

        return false;
    }

}
