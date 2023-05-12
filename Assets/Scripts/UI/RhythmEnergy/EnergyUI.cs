using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnergyUI : MonoBehaviour
{
    public Player player;
    [Header("카운터 설정")]
    [SerializeField] private GameObject counterPrefab;
    [SerializeField] public float gap = 30;
    private int tempEnergy = 0;
    private int energy = 0;
    private int level = 0;

    private List<EnergyCounterUI> counters;

    private void Awake()
    {
        if (player == Player.Player1) Global.OnP1EnergyChange += OnEnergyChange;
        else if (player == Player.Player2) Global.OnP2EnergyChange += OnEnergyChange;

        if (transform.childCount == 0)
        {
            CreateChildren(Global.maxEnergy);
        }
        else counters = GetComponentsInChildren<EnergyCounterUI>().ToList();

        if (counters.Count == 0) enabled = false;
    }

    private void OnEnergyChange()
    {
        int newEnergy = player == Player.Player1?Global.GetP1Energy(): Global.GetP2Energy();
        if(newEnergy == Global.maxEnergy && newEnergy == energy) // 에너지 꽉찼을때
        {
            transform.DORewind();
            transform.DOShakePosition(0.4f, new Vector3(7, 7, 0), 27).OnComplete(() => transform.DOLocalMove(Vector3.zero, 0.2f));
            return;
        }
        if (newEnergy > counters.Count || newEnergy == energy) return; // 에너지 변화없거나 카운터가 부족할때
        if (newEnergy > energy) // 에너지 증가했을때
        {
            for (int i = energy; i < newEnergy; i++)
            {
                counters[i].Activate(level,0);
            }
            level = CheckLevel(newEnergy);
        }
        else if(newEnergy < energy) // 에너지 적어졌을때
        {
            level = CheckLevel(newEnergy);
            for (int i = Mathf.Max(0,newEnergy); i < energy; i++)
            {
                counters[i].SetLevel(level, 0);
                counters[i].Deactivate();
                transform.DORewind();
                transform.DOShakePosition(0.4f,new Vector3(5, 5, 0), 25).OnComplete(() => transform.DOLocalMove(Vector3.zero,0.2f));
            }
        }
        energy = newEnergy;
    }

    private int CheckLevel(int _energy)
    {
        int newLevel = (int)(player == Player.Player1 ? Global.GetP1EnergyLevel() : Global.GetP2EnergyLevel());
        if (newLevel == level) return level;
        SetLevel(newLevel);
        return newLevel;
    }

    private void SetLevel(int level)
    {
        for (int i = 0; i < energy; i++)
        {
            counters[i].SetLevel(level, i * 0.05f + 0.1f);
        }
    }

    [ContextMenu("create 10")]
    private void CreateChildrenEditor() => CreateChildren(10);
    private void CreateChildren(int count)
    {
        counters = new();
        for (int i = 0; i < count; i++)
        {
            EnergyCounterUI instance = Instantiate(counterPrefab, transform).GetComponent<EnergyCounterUI>();
            counters.Add(instance);
            instance.transform.localPosition = new(gap * i, 0, 0);
        }
    }
}
