using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnergyUI : MonoBehaviour
{
    public Player player;
    private int tempEnergy = 0;
    private int energy = 0;
    private int level = 0;

    private List<EnergyCounterUI> counters;

    private void Awake()
    {
        if (player == Player.Player1) Global.OnP1EnergyChange += OnEnergyChange;
        else if (player == Player.Player2) Global.OnP2EnergyChange += OnEnergyChange;

        counters = GetComponentsInChildren<EnergyCounterUI>().ToList();
        if (counters.Count == 0) enabled = false;

        Global.P1RightAction += TestEnergyUp;
        Global.P1LeftAction += TestEnergyClear;
        Global.P1UpAction += TestSetLevel;
    }

    // 테스트용
    private void TestEnergyUp() { tempEnergy++; Global.OnP1EnergyChange?.Invoke(); }
    private void TestEnergyClear() { tempEnergy = 0; Global.OnP1EnergyChange?.Invoke(); }
    private void TestSetLevel()
    {
        level = (level + 1) % 4;
    }

    private void OnEnergyChange()
    {
        int newEnergy = tempEnergy; // 수정필요
        if (newEnergy > counters.Count || newEnergy == energy) return;
        if (newEnergy > energy)
        {
            for (int i = energy; i < newEnergy; i++)
            {
                counters[i].Activate(level,0);
            }
        }
        else
        {
            for (int i = energy-1; i >= newEnergy-1 && i>=0; i--)
            {
                counters[i].SetLevel(level, 0);
                counters[i].Deactivate();
                transform.DORewind();
                transform.DOShakePosition(0.4f,new Vector3(5, 5, 0), 15).OnComplete(() => transform.DOLocalMove(Vector3.zero,0.2f));
            }
        }
        energy = newEnergy;
        level = CheckLevel(newEnergy);
    }

    private int CheckLevel(int _energy)
    {
        int newLevel = 0;
        if (_energy > 2) newLevel = 1;
        if (_energy > 3) newLevel = 2;
        if (_energy > 4) newLevel = 3;
        if (newLevel != level) SetLevel(newLevel);
        return newLevel;
    }

    private void SetLevel(int level)
    {
        for (int i = 0; i < energy; i++)
        {
            counters[i].SetLevel(level, i * 0.05f + 0.1f);
        }
    }
}
