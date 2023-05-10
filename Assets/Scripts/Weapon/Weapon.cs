using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int damage = 0;

    public abstract void P1ShootForward();
    public abstract void P1ShootUpDown();
    public abstract void P2ShootForward();
    public abstract void P2ShootUpDown();
    public abstract void ShootFail();

}
