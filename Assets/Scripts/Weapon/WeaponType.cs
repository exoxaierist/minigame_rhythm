using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponType : MonoBehaviour
{
    public bool isFree = true;
    public AttackInfo payload;
    public Wtype wtype;

    public abstract void SetInfo(Vector3 position, Vector3 direction, Player p, float len);
    public abstract void Disable();
}
