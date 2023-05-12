using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public AttackInfo payload;
    public float length;
    public Vector3 dir;

    private LineRenderer line;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        LaserSettings();
    }

    //todo
    //레이저 이펙트 및 피격 판정

    private void LaserSettings()
    {
        line.SetPosition(0, dir);
        line.SetPosition(1, dir * length + dir);
        Destroy(gameObject, 2f);
    }
}
