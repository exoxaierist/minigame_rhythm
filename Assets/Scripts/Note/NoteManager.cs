using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using DG.Tweening;

public class NoteManager : MonoBehaviour
{
    [Header("Mid�����̸�")] public string midiFileName;
    [Header("��Ʈ ��� �ð�")] public float notePlayingTime = 2f; // ��Ʈ�� �������� �ð�
    private MidiFile midiFile;
    public GameObject note;
    public List<double> hitTimes = new List<double>();
    int hitArray = 0; // ������ ��Ʈ ��
    float correctionValue = 0; // ���� ��  
    public AudioSource bgm;
    
    //ȣ��� ����
    Transform noteIndex;
    Transform tempNote;
    Transform detectionLine;
    RectTransform noteUI;
    Coroutine coroutine;
    Image detLineIma;
    Image left;
  
    //���� ����
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
            Debug.Log("���� �����");
            coroutine = StartCoroutine(PlayNote());
        }
    }

    /** ��Ʈ ���(�ڷ�ƾ) */
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

    /** ���� ���*/
    void PlaySong()
    {
        bgm.Play();
    }

    //int Detection()

    /** ��Ʈ ���� ��� */
    bool Detection()
    {
        
        if (hitArray == hitTimes.Count) //������ ��Ʈ
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
        else //ó�� ��Ʈ
        {
            if (hitTimes[hitArray] - 0.1f + correctionValue <= bgm.time)
                return true;
        }
            
        return false;
    }

    /** ��ũ ���߱� �� ��Ʈ ����*/
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

    /** �Ϲ� ��Ʈ ����*/
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

    /** Midi���� �ҷ����� */
    void GetMidiFile(string name)
    {
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + name + ".mid");
    }

    /** Midi���Ϸκ��� ���� �ҷ����� */
    void GetDataFromMidi()
    {
        hitTimes = new List<double>();
        var notes = midiFile.GetNotes();
        TempoMap tempoMap = midiFile.GetTempoMap();
        foreach (Note note in notes)
        {        
            // ��Ʈ�� �����ϴ� �ð�
            double noteStart = note.TimeAs<MetricTimeSpan>(tempoMap).TotalSeconds;

            // ��Ʈ�� ������ �ϴ� �ð�
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
            Debug.Log("���� ����");
            isPlaying = false;
            StopCoroutine(coroutine);
            coroutine = null;
            return;
        }          
    }
}
