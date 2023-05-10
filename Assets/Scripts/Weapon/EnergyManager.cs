using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    public int maxEnergy = 10;
    private int p1Energy = 0;
    private int p2Energy = 0;

    private void Awake()
    {
        Global.energyManager = this;
    }

    public void IncP1Energy()
    {
        if (p1Energy == maxEnergy) return;
        p1Energy = Mathf.Min(p1Energy + 1, maxEnergy);
        Global.OnP1EnergyChange?.Invoke();
    }
    public void IncP2Energy()
    {
        if (p2Energy == maxEnergy) return;
        p2Energy = Mathf.Min(p2Energy + 1, maxEnergy);
        Global.OnP2EnergyChange?.Invoke();
    }

    public void ResetP1Energy() 
    {
        if (p1Energy == 0) return;
        p1Energy = 0;
        Global.OnP1EnergyChange?.Invoke();
    }
    public void ResetP2Energy() 
    {
        if (p2Energy == 0) return;
        p2Energy = 0;
        Global.OnP2EnergyChange?.Invoke();
    }


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
