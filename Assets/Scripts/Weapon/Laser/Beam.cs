using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : WeaponType
{
    public AttackInfo payload;
    public Vector3 pos;
    public Vector3 dir;
    public float length;

    [SerializeField] float chargeTime = 0.3f;

    private LineRenderer line;
    private LayerMask hitMask;

    //todo
    //레이저 이펙트 및 피격 판정

    private void LaserSettings()
    {
        hitMask = payload.owner == Player.Player1 ? 1 << LayerMask.NameToLayer("P2") : 1 << LayerMask.NameToLayer("P1");
        line = GetComponent<LineRenderer>();

        line.startWidth = Global.gridIncrement;
        line.endWidth = Global.gridIncrement;
        line.SetPosition(0, dir);
        line.SetPosition(1, dir * length + dir);

        line.SetColors(Color.red, Color.red);
    }

    IEnumerator HitBox()
    {
        //float time = 0;

        DOTween.To(() => line.startWidth, x => line.startWidth = x, 0, chargeTime).SetEase(Ease.InCirc);
        DOTween.To(() => line.endWidth, x => line.endWidth = x, 0, chargeTime).SetEase(Ease.InCirc);

        yield return new WaitForSeconds(chargeTime);

        line.startWidth = Global.gridIncrement;
        line.endWidth = Global.gridIncrement;
        //임시
        line.SetColors(Color.blue, Color.blue);

        //while (time <= holdTime)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, length, hitMask);

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
                //피격
                //작동 잘함
            }
            //time += Time.deltaTime;
        }

        yield return new WaitForSeconds(0.1f);
        Disable();
    }


    public override void SetInfo(Vector3 position, Vector3 direction, Player p, float len)
    {
        transform.position = position;
        dir = direction;
        length = len;
        payload.owner = p;
        isFree = false;

        LaserSettings();
        StartCoroutine(HitBox());
        //Invoke("Disable", 1f);
    }
    protected override void Disable()
    {
        isFree = true;
        gameObject.SetActive(false);
    }
}
