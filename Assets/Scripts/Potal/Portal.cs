using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform destination;
    [Header("ȸ����(���� ���� �� ���� �ȵ�)")] public int RotationValue;
    [Header("���� ���� �� ����")]
    public bool isLocking = false;
    public Vector2 Direction = Vector2.down;   
}

