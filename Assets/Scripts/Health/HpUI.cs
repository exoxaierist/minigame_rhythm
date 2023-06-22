using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUI : MonoBehaviour
{
    public HpUIType type;
    public float gap = 0.2f;
    private int maxHp = 10;
    private int hp;
    private readonly List<HpCounterUI> counters = new();
    private Vector3 origin;

    private Player player;
    private GameObject p1HP;
    private GameObject p2HP;

    public void Set(int _maxHP, int _hp, HpUIType _type,Player _player)
    {
        origin = transform.localPosition;
        transform.localScale = transform.lossyScale;
        maxHp = _maxHP;
        hp = _maxHP;
        type = _type;
        player = _player;

        if (type == HpUIType.Counter)
        {
            CreateCounters();
            if (maxHp != hp) SetHP(hp);
        }else if(type == HpUIType.Fixed)
        {
            p1HP = GameObject.FindGameObjectWithTag("P1HP");
            p2HP = GameObject.FindGameObjectWithTag("P2HP");
            if (player==Player.Player1) for (int i = 0; i < p1HP.transform.childCount; i++)counters.Add(p1HP.transform.GetChild(i).GetComponent<HpCounterUI>());
            else for (int i = 0; i < p2HP.transform.childCount; i++)counters.Add(p2HP.transform.GetChild(i).GetComponent<HpCounterUI>());
        }
    }

    public void CreateCounters()
    {
        // destroy all counters
        while(counters.Count>0)
        {
            Destroy(counters[0]);
        }
        // create new counters
        for (int i = 0; i < maxHp; i++)
        {
            GameObject instance = Instantiate(Global.assets.hpCounterUI, transform);
            instance.transform.SetParent(transform);
            instance.transform.localPosition = new Vector3(((maxHp - 1) * gap) * -0.5f + i * gap, 0, 0);
            instance.GetComponent<HpCounterUI>().SetOrigin();
            counters.Add(instance.GetComponent<HpCounterUI>());
        }
    }

    public void SetHP(int newHp)
    {
        if (newHp == hp) return;
        if(type == HpUIType.Counter)
        {
            transform.DOShakePosition(0.1f, new Vector3(0.05f,0.05f,0), 20).OnComplete(() => transform.localPosition = origin);
            if (newHp > hp)
            {
                for (int i = 0; i < newHp-hp; i++)
                {
                    counters[hp + i].SetFull(i*0.06f);
                }
            }
            else
            {
                for (int i = 0; i < hp-newHp; i++)
                {
                    counters[hp - 1 - i].SetEmpty(i*0.06f);
                }
            }
        }else if (type == HpUIType.Fixed)
        {
            if (newHp > hp)
            {
                for (int i = 0; i < newHp - hp; i++)
                {
                    counters[hp + i].SetFullSprite();
                }
            }
            else
            {
                for (int i = 0; i < hp - newHp; i++)
                {
                    counters[hp - 1 - i].SetEmptySprite();
                }
            }
        }
        hp = newHp;
    }
}
