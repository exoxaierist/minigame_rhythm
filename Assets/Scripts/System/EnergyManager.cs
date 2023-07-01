using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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

    private void Start()
    {
        if (Global.CheckBeat == null || GameObject.FindGameObjectWithTag("P1") == null) { enabled = false; return; }
        Global.GetP1Energy = GetP1Energy;
        Global.GetP2Energy = GetP2Energy;
        Global.GetP1EnergyLevel = GetP1EnergyLevel;
        Global.GetP2EnergyLevel = GetP2EnergyLevel;
        Global.P1AnyAction += OnP1Any;
        Global.P2AnyAction += OnP2Any;
        maxEnergy = Global.maxEnergy;
    }

    private void OnP1Any()
    {
        if (Global.CheckBeat()) IncP1Energy();
        else DecP1Energy();
    }

    private void OnP2Any()
    {
        if (Global.CheckBeat()) IncP2Energy();
        else DecP2Energy();
    }

    public void IncP1Energy()
    {
        p1Energy = Mathf.Min(p1Energy + 1, maxEnergy);
        Global.OnP1EnergyChange?.Invoke();
    }
    public void IncP2Energy()
    {
        p2Energy = Mathf.Min(p2Energy + 1, maxEnergy);
        Global.OnP2EnergyChange?.Invoke();
    }
    public void DecP1Energy()
    {
        p1Energy = Mathf.Clamp(p1Energy - 1, 0, maxEnergy);
        Global.OnP1EnergyChange?.Invoke();
    }
    public void DecP2Energy()
    {
        p2Energy = Mathf.Clamp(p2Energy - 1, 0, maxEnergy);
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

    public void ChangeEnergy()
    {
        int temp = p2Energy;
        p2Energy = p1Energy;
        p1Energy = temp;

        Global.OnP1EnergyChange?.Invoke();
        Global.OnP2EnergyChange?.Invoke();
    }

    public void EnergyMax(Player player)
    {
        if(player == Player.Player1)
        {
            p1Energy = maxEnergy;
            Global.OnP1EnergyChange?.Invoke();
        }
        else if(player == Player.Player2)
        {
            p2Energy = maxEnergy;
            Global.OnP2EnergyChange?.Invoke();
        }
    }

    public int GetP1Energy() => p1Energy;
    public int GetP2Energy() => p2Energy;

    private RhythmLevel GetP1EnergyLevel()
    {
        if (p1Energy < 1) return RhythmLevel.Zero;
        else if (p1Energy < 4) return RhythmLevel.One;
        else if (p1Energy < 8) return RhythmLevel.Two;
        else return RhythmLevel.Three;
    }

    private RhythmLevel GetP2EnergyLevel()
    {
        if (p2Energy < 1) return RhythmLevel.Zero;
        else if (p2Energy < 4) return RhythmLevel.One;
        else if (p2Energy < 8) return RhythmLevel.Two;
        else return RhythmLevel.Three;
    }
}
