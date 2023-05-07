using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Laser : Weapon
{
    [Header("���, ������")]
    [SerializeField]
    LineRenderer dangerLine;
    [SerializeField]
    LineRenderer laser;

    [Header("�ִ� �Ÿ�")]
    public int maxLen = 10;

    private LayerMask mask;

    //�ӽ� ȿ�� - ��
    Color c = Color.red;

    private void Start()
    {
        mask = 1 << LayerMask.NameToLayer("Portal") | 1 << LayerMask.NameToLayer("Wall");
    }
    private void Update()
    {
        CheckEnergy();
        if (isShooting)
            transform.position = pos;
    }

    public override void Shoot(KeyCode key)
    {
        if (rhythmLevel == RhythmLevel.Zero || isShooting)
            return;

        if (key == KeyCode.F)
            CalculateLaser(Vector3.right);
        else if (key == KeyCode.G)
            CalculateLaser(Vector3.up);

        LaserEffectByLevel(rhythmLevel);
        StartCoroutine(ShootFunc());

        ResetEnergy();
    }

    //�ӽ�
    private void LaserEffectByLevel(RhythmLevel rl)
    {
        switch (rl)
        {
            case RhythmLevel.One:
                damage = 1;
                c = Color.red;
                break;
            case RhythmLevel.Two:
                damage = 2;
                c = Color.green;
                break;
            case RhythmLevel.Three:
                damage = 3;
                c = Color.blue;
                break;
        }
    }

    //�ӽ�
    IEnumerator ShootFunc()
    {
        isShooting = true;
        pos = transform.position;

        dangerLine.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        dangerLine.gameObject.SetActive(false);

        laser.startColor = c;
        laser.endColor = c;
        laser.enabled = true;
        yield return new WaitForSeconds(3f);
        laser.enabled = false;
        isShooting = false;

        transform.position = transform.parent.position;
    }

    //������ ��ġ �� �Ÿ� ���
    private void CalculateLaser(Vector3 dir)
    {
        if (player == Player.Player2)
            dir *= -1;

        dangerLine.SetPosition(0, dir);
        laser.SetPosition(0, dir);

        float distance = maxLen;

        RaycastHit2D hit = Physics2D.Raycast(transform.position + dir, dir, float.MaxValue, mask);
        if (hit.collider != null)
        {
            distance = Vector2.Distance(transform.position + dir, hit.point);
            Debug.Log(hit.collider.name + " " + distance);

            //TODO ��Ż �浹 ��
        }

        dangerLine.SetPosition(1, dir * distance + dir);
        laser.SetPosition(1, dir * distance + dir);
    }
}
