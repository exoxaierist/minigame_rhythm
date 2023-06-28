using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 공격 받아들이는거
public interface IReceiveAttack
{
    public void OnAttack(AttackInfo info) { }
}
