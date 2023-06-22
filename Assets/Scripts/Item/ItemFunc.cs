using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFunc : MonoBehaviour
{
    public Dictionary<ItemType, IEnumerator> itemDic = new Dictionary<ItemType, IEnumerator>();

    private void Awake()
    {
        itemDic.Add(ItemType.item1, Item1());
        itemDic.Add(ItemType.item2, Item2());
        itemDic.Add(ItemType.item3, Item3());
    }

    IEnumerator Item1()
    {
        yield return null;
        Hp hp = GetComponent<Hp>();
        if (hp != null)
            Debug.Log("item1");
    }

    IEnumerator Item2()
    {
        yield return null;
        Hp hp = GetComponent<Hp>();
        if (hp != null)
            Debug.Log("item1");
    }

    IEnumerator Item3()
    {
        yield return null;
        Hp hp = GetComponent<Hp>();
        if (hp != null)
            Debug.Log("item1");
    }
}
