using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFunc : MonoBehaviour
{
    ItemType ownedItem = ItemType.None;

    [SerializeField] SpriteRenderer itemSlot;

    private void Awake()
    {
        ownedItem = ItemType.None;
        Player p = GetComponent<PlayerBase>().player;
        if(p == Player.Player1)
        {
            Global.P1UseItem -= UseItem;
            Global.P1UseItem += UseItem;
        }
        else if (p == Player.Player2)
        {
            Global.P2UseItem -= UseItem;
            Global.P2UseItem += UseItem;
        }
    }

    public void StoreItem(ItemType itype)
    {
        ownedItem = itype;

        //���� ���� ���ڸ��� ����̸� ��� �� �ٷ� ����
        if (itype == ItemType.Shield) { UseItem(); return; }

        ChangeItemSlot(itype);
    }

    public void UseItem()
    {
        // TODO ���̻��̿� �����ۺ� �Լ� �ۼ�
        switch (ownedItem)
        {
            case ItemType.item1:
                break;
            case ItemType.item2:
                break;
            case ItemType.item3:
                break;
        }

        ownedItem = ItemType.None;
        ChangeItemSlot(ItemType.None);
    }

    public void ChangeItemSlot(ItemType item)
    {
        itemSlot.sprite = Global.assets.itemImg[(int)item];
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
