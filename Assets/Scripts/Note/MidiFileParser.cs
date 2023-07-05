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
    [Header("Mid파일이름")] public string midiFileName;
    private MidiFile midiFile;
    
    
    /** Midi파일 불러오기 */
    public void GetMidiFile(string name)
    {
        if(MidiFile.Read(Application.streamingAssetsPath + "/" + name + ".mid") != null)
        {
            midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + name + ".mid");
        }
        else
            midiFile = MidiFile.Read(Application.streamingAssetsPath + "/bgm1.mid");
    }

    /** Midi파일로부터 정보 불러오기 */
    public List<midiInfo> GetDataFromMidi()
    {
        List<midiInfo> hitTimes = new List<midiInfo>();
        midiInfo info = new midiInfo();
        var notes = midiFile.GetNotes();
        TempoMap tempoMap = midiFile.GetTempoMap();
        
        foreach (Note note in notes)
        {
            
            // 노트가 시작하는 시간
            info.hitTime = (float)note.TimeAs<MetricTimeSpan>(tempoMap).TotalSeconds;

            // 노트 음계
            info.noteName = note.NoteName;

            hitTimes.Add(info);

        }
        return hitTimes;
    }
}
