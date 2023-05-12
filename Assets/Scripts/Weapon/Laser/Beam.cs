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
    //������ ����Ʈ �� �ǰ� ����

    private void LaserSettings()
    {
        line.SetPosition(0, dir);
        line.SetPosition(1, dir * length + dir);
        Destroy(gameObject, 2f);
    }
}
