using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform destination;
    [Header("회전값(방향 고정 시 적용 안됨)")] public int RotationValue;
    [Header("방향 고정 및 방향")]
    public bool isLocking = false;
    public Vector2 Direction = Vector2.down;   
}

