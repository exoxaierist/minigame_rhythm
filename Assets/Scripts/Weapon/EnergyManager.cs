using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    private int p1Energy = 0;
    private int p2Energy = 0;

    public void IncP1Energy() => p1Energy++;
    public void IncP2Energy() => p2Energy++;

    public void ResetP1Energy() => p1Energy = 0;
    public void ResetP2Energy() => p2Energy = 0;

    public RhythmLevel GetP1Energy()
    {
        if (p1Energy < 1) return RhythmLevel.Zero;
        else if (p1Energy < 4) return RhythmLevel.One;
        else if (p1Energy < 8) return RhythmLevel.Two;
        else return RhythmLevel.Three;
    }

    public RhythmLevel GetP2Energy()
    {
        if (p2Energy < 1) return RhythmLevel.Zero;
        else if (p2Energy < 4) return RhythmLevel.One;
        else if (p2Energy < 8) return RhythmLevel.Two;
        else return RhythmLevel.Three;
    }
}
