using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 공격할때 전달하는 공격정보. 공격한 주체나 기타정보 전달용. 피격 이펙트등 구현할때 쓸수 있는 구조체
[Serializable]
public struct AttackInfo
{
    public Player owner;
    public int damage;
}
