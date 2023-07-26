using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int damage = 1;
    public Player player = Player.Player1;

    private void Awake()
    {
        player = GetComponent<PlayerBase>().player;
        if (player == Player.Player1)
        {
            Global.P1PrimaryAction -= P1ShootForward;
            Global.P1SecondaryAction -= P1ShootVertical;
            Global.P1PrimaryAction += P1ShootForward;
            Global.P1SecondaryAction += P1ShootVertical;
        }
        else
        {
            Global.P2PrimaryAction -= P2ShootForward;
            Global.P2SecondaryAction -= P2ShootVertical;
            Global.P2PrimaryAction += P2ShootForward;
            Global.P2SecondaryAction += P2ShootVertical;
        }
    }

    public abstract void P1ShootForward();
    public abstract void P1ShootVertical();
    public abstract void P2ShootForward();
    public abstract void P2ShootVertical();

}
