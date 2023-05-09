using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using DG.Tweening;

public class NoteManager : MonoBehaviour
{
    [Header("Mid파일이름")] public string midiFileName;
    [Header("노트 재생 시간")] public float notePlayingTime = 2f; // 노트가 지나가는 시간
    private MidiFile midiFile;
    public GameObject note;
    public List<double> hitTimes = new List<double>();
    int hitArray = 0; // 지나간 노트 수
    float correctionValue = 0; // 보정 값  
    public AudioSource bgm;
    
    //호출용 변수
    Transform noteIndex;
    Transform tempNote;
    Transform detectionLine;
    RectTransform noteUI;
    Coroutine coroutine;
    Image detLineIma;
    Image left;
  
    //상태 변수
    bool isPlaying;

    private void Awake()
    {
        //Global.GetTimingms += Detection;
    }

    void Start()
    {
        noteIndex = transform.Find("UI").Find("NoteIndex");
        detectionLine = transform.Find("UI").Find("DetectionLine");
        noteUI = transform.Find("UI").GetComponent<RectTransform>();
        detLineIma = detectionLine.GetComponent<Image>();
        left = transform.Find("UI").Find("Left").GetComponent<Image>();
        hitArray = 0;
        isPlaying = true;
        coroutine = StartCoroutine(PlayNote());
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
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
        }

        if (!isPlaying && !bgm.isPlaying && coroutine==null) 
        {
            Debug.Log("음악 재시작");
            coroutine = StartCoroutine(PlayNote());
        }
    }

    /** 노트 재생(코루틴) */
    IEnumerator PlayNote()
    {
        hitArray = 0;
        correctionValue = 0;
        isPlaying = true;
        GetMidiFile(midiFileName);
        GetDataFromMidi();
        CreatSyncNote();
        yield return new WaitForEndOfFrame();
        float temp = 0;
        for (int count = 0; count < hitTimes.Count; count++)
        {
            yield return new WaitForSeconds((float)hitTimes[count] - temp);
            temp = (float)hitTimes[count];
            CreatNote();
        }
    }

    /** 음악 재생*/
    void PlaySong()
    {
        bgm.Play();
    }

    //int Detection()

    /** 노트 판정 계산 */
    bool Detection()
    {
        
        if (hitArray == hitTimes.Count) //마지막 노트
        {
            if (hitTimes[hitArray - 1] + 0.1f + correctionValue >= bgm.time && hitTimes[hitArray - 1] - 0.1f + correctionValue <= bgm.time)
                return true;               
        }
        else if(hitArray > 0) 
        {
            if (hitTimes[hitArray - 1] + correctionValue >= bgm.time - 0.1f)
                return true;        
            if (hitTimes[hitArray] + correctionValue <= bgm.time + 0.1f)
                return true;
        }
        else //처음 노트
        {
            if (hitTimes[hitArray] - 0.1f + correctionValue <= bgm.time)
                return true;
        }
            
        return false;
    }

    /** 싱크 맞추기 용 노트 생성*/
    void CreatSyncNote()
    {       
        tempNote = Instantiate(note, noteUI.anchoredPosition, Quaternion.identity).transform;
        Image SinkNote = tempNote.GetComponent<Image>();
        Color c = SinkNote.color; c.a = 0;
        tempNote.GetComponent<Image>().color = c;
        tempNote.GetComponent<RectTransform>().anchoredPosition = new Vector2(noteUI.rect.width / 2, -400);        
        tempNote.SetParent(noteIndex, false);
        tempNote.DOMoveX(detectionLine.position.x, notePlayingTime).SetEase(Ease.Linear).OnComplete(() => { PlaySong(); Destroy(noteIndex.GetChild(0).gameObject); });
    }

    /** 일반 노트 생성*/
    void CreatNote(int dir = 1)
    {
        tempNote = Instantiate(note, noteUI.anchoredPosition, Quaternion.identity).transform;
        tempNote.GetComponent<RectTransform>().anchoredPosition = new Vector2(dir * noteUI.rect.width/2, -400);
        tempNote.SetParent(noteIndex, false);
        tempNote.GetComponent<Image>().DOFade(1.0f, notePlayingTime);
        if (dir == 1)
        {
            tempNote.DOMoveX(detectionLine.position.x, notePlayingTime).SetEase(Ease.Linear).OnComplete(() => RemoveNote());
            CreatNote(-1);
        }
        else tempNote.DOMoveX(detectionLine.position.x, notePlayingTime).SetEase(Ease.Linear);      
    }

    /** Midi파일 불러오기 */
    void GetMidiFile(string name)
    {
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + name + ".mid");
    }

    /** Midi파일로부터 정보 불러오기 */
    void GetDataFromMidi()
    {
        hitTimes = new List<double>();
        var notes = midiFile.GetNotes();
        TempoMap tempoMap = midiFile.GetTempoMap();
        foreach (Note note in notes)
        {        
            // 노트가 시작하는 시간
            double noteStart = note.TimeAs<MetricTimeSpan>(tempoMap).TotalSeconds;

            // 노트가 눌려야 하는 시간
            double hitTime = noteStart;

            hitTimes.Add(hitTime);
            
        }
    }

    void RemoveNote()
    {       
        Destroy(noteIndex.GetChild(1).gameObject);
        Destroy(noteIndex.GetChild(0).gameObject);      
        correctionValue = bgm.time - (float)hitTimes[hitArray] - 0.05f;
        if (hitTimes.Count > hitArray) hitArray++;
        if (hitTimes.Count == hitArray)
        {
            Debug.Log("음악 종료");
            isPlaying = false;
            StopCoroutine(coroutine);
            coroutine = null;
            return;
        }          
    }
}
