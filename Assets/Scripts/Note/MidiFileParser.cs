using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

public struct midiInfo {
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteName;
    public float hitTime;
};
public class MidiFileParser : MonoBehaviour
{
    [Header("Mid�����̸�")] public string midiFileName;
    private MidiFile midiFile;
    
    
    /** Midi���� �ҷ����� */
    public void GetMidiFile(string name)
    {
        if(MidiFile.Read(Application.streamingAssetsPath + "/" + name + ".mid") != null)
        {
            midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + name + ".mid");
        }
        else
            midiFile = MidiFile.Read(Application.streamingAssetsPath + "/bgm1.mid");
    }

    /** Midi���Ϸκ��� ���� �ҷ����� */
    public List<midiInfo> GetDataFromMidi()
    {
        List<midiInfo> hitTimes = new List<midiInfo>();
        midiInfo info = new midiInfo();
        var notes = midiFile.GetNotes();
        TempoMap tempoMap = midiFile.GetTempoMap();
        
        foreach (Note note in notes)
        {
            
            // ��Ʈ�� �����ϴ� �ð�
            info.hitTime = (float)note.TimeAs<MetricTimeSpan>(tempoMap).TotalSeconds;

            // ��Ʈ ����
            info.noteName = note.NoteName;

            hitTimes.Add(info);

        }
        return hitTimes;
    }
}
