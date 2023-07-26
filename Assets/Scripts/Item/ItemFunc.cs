using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemFunc : MonoBehaviour
{
    [SerializeField] ItemType ownedItem = ItemType.None;
    [SerializeField] Player p = Player.Player1;

    [SerializeField] Image itemSlot;
    Hp hp;
    Laser laser;

    private void Awake()
    {
        ownedItem = ItemType.None;
        p = GetComponent<PlayerBase>().player;
        hp = GetComponent<Hp>();
        laser = GetComponent<Laser>();

        if(p == Player.Player1)
        {
            Global.P1UseItem -= UseOwnedItem;
            Global.P1UseItem += UseOwnedItem;
            itemSlot = GameObject.FindGameObjectWithTag("P1ItemSlot").GetComponent<Image>();
        }
        else if (p == Player.Player2)
        {
            Global.P2UseItem -= UseOwnedItem;
            Global.P2UseItem += UseOwnedItem;
            itemSlot = GameObject.FindGameObjectWithTag("P2ItemSlot").GetComponent<Image>();
        }

        Global.OnReset -= ResetFunc;
        Global.OnReset += ResetFunc;
    }

    public void StoreItem(ItemType itype)
    {
        //faint 같이 먹자마자 사용이면 사용 후 바로 리턴
        if (itype == ItemType.Faint || itype == ItemType.Heal || itype == ItemType.Bang)
        {
            UseBurstItem(itype);
            return;
        }

        ChangeItemSlot(itype);
    }

    //즉발 아이템
    public void UseBurstItem(ItemType itype)
    {
        switch (itype)
        {
            case ItemType.Heal:
                hp.AddToHP(1);
                break;
            case ItemType.Bang:
                hp.AddToHP(-1);
                break;
            case ItemType.Faint:
                FaintItem();
                break;
        }
    }

    //사용 아이템
    public void UseOwnedItem()
    {
        if (!Global.CheckBeat()) return;

        switch (ownedItem)
        {
            case ItemType.Shield:
                int energy = p == Player.Player1 ? Global.GetP1Energy() : Global.GetP2Energy();
                if (energy == 0) return;
                ShieldItem();
                break;
            case ItemType.MaxEnergy:
                MaxEnergyItem();
                break;
            case ItemType.ChangeEnergy:
                ChangeEnergyItem();
                break;
            case ItemType.Cross:
                CrossItem();
                break;
            case ItemType.Chorus:
                ChorusItem();
                break;
        }

        ChangeItemSlot(ItemType.None);
    }

    public void ChangeItemSlot(ItemType item)
    {
        ownedItem = item;
        itemSlot.sprite = Global.assets.itemImg[(int)item];
    }

    #region "아이템 효과"
    private void ShieldItem()
    {
        hp.ShieldDeploy(5f);
    }

    private void MaxEnergyItem()
    {
        Global.energyManager.EnergyMax(p);
    }

    private void ChangeEnergyItem()
    {
        Global.energyManager.ChangeEnergy();
    }

    private void FaintItem()
    {
        GameObject faint = Instantiate(Global.assets.Faint);
        faint.transform.position = transform.position;
        faint.GetComponent<Faint>().setTarget(p);
    }

    private void CrossItem()
    {
        if(laser == null)
            return;
        laser.CrossLaser();
    }

    private void ChorusItem()
    {
        Chorus chorus = Instantiate(Global.assets.Chorus).GetComponent<Chorus>();
        chorus.Init(p, laser.maxLen, transform.position);
    }
    #endregion

    private void ResetFunc()
    {
        ChangeItemSlot(ItemType.None);
    }
}
