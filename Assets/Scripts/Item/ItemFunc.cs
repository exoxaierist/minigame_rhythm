using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFunc : MonoBehaviour
{
    ItemType ownedItem = ItemType.None;
    Player p = Player.Player1;

    [SerializeField] SpriteRenderer itemSlot;
    Hp hp;

    private void Awake()
    {
        ownedItem = ItemType.None;
        p = GetComponent<PlayerBase>().player;
        hp = GetComponent<Hp>();

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

        //쉴드 같이 먹자마자 사용이면 사용 후 바로 리턴
        if (itype == ItemType.Faint || itype == ItemType.Heal || itype == ItemType.Bang) 
        {
            UseItem();
            return; 
        }

        ChangeItemSlot(ownedItem);
    }

    public void UseItem()
    {
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
            case ItemType.Heal:
                hp.AddToHP(1);
                break;
            case ItemType.Bang:
                hp.AddToHP(-1);
                break;
        }

        ownedItem = ItemType.None;
        ChangeItemSlot(ItemType.None);
    }

    public void ChangeItemSlot(ItemType item)
    {
        itemSlot.sprite = Global.assets.itemImg[(int)item];
    }

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
}
