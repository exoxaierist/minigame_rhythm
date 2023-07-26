using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : WeaponType
{
    public Vector3 pos;
    public Vector3 dir;
    public float length;

    [SerializeField] float chargeTime = 0.05f;

    private LineRenderer line;
    private LayerMask hitMask;

    private Color applyCol;
    private Color p1Color = new(0, 0.43f, 0.89f);
    private Color p2Color = new(0.956f, 0.25f, 0.11f);

    private SpriteRenderer circleStart;
    private SpriteRenderer circleEnd;


    public void LaserStart()
    {
        line.SetPosition(0, dir * 0.8f);
        //circleStart.transform.localPosition = line.GetPosition(0);
        circleStart.color = new(circleStart.color.r, circleStart.color.g, circleStart.color.b, 0);
        circleStart.DOFade(1, chargeTime);
        circleStart.transform.localScale = new(4f, 4f, 4f);
        circleStart.transform.DOScale(0.4f, chargeTime);
    }

    private void LaserSettings()
    {
        if (payload.owner == Player.Player2) applyCol = p2Color;
        else applyCol = p1Color;
        hitMask = payload.owner == Player.Player1 ? 1 << LayerMask.NameToLayer("P2") : 1 << LayerMask.NameToLayer("P1");
        line = GetComponent<LineRenderer>();

        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, dir * (length+1));

        line.startColor = applyCol;
        line.endColor = applyCol;

        circleStart = Instantiate(Global.assets.laserEffect,transform).GetComponent<SpriteRenderer>();
        circleEnd = Instantiate(Global.assets.laserEffect,transform).GetComponent<SpriteRenderer>();

        circleStart.transform.localScale = new(0.4f, 0.4f, 0.4f);
        circleEnd.transform.localScale = new(0f, 0f, 0f);

        circleStart.transform.position = transform.position + line.GetPosition(0);
        circleEnd.transform.position = transform.position + line.GetPosition(1);

        circleStart.color = applyCol;
        circleEnd.color = applyCol;
    }

    IEnumerator HitBox()
    {
        Global.CamShakeSmall();
        //float time = 0;

        line.startWidth = 0f;
        line.endWidth = 0f;

        DOTween.To(() => line.startWidth, x => line.startWidth = x, 0f, chargeTime).SetEase(Ease.InCirc);
        DOTween.To(() => line.endWidth, x => line.endWidth = x, 0f, chargeTime).SetEase(Ease.InCirc);

        yield return new WaitForSeconds(chargeTime);
        Global.CamShakeMedium();

        line.startWidth = 0.4f;
        line.endWidth = 0.4f;

        DOTween.To(() => line.startWidth, x => line.startWidth = x, 0.1f, chargeTime).SetEase(Ease.InCirc);
        DOTween.To(() => line.endWidth, x => line.endWidth = x, 0.1f, chargeTime).SetEase(Ease.InCirc);

        circleStart.transform.localScale = new(0.4f, 0.4f, 0.4f);
        circleStart.transform.localPosition = line.GetPosition(0);
        circleEnd.transform.localScale = new(0.4f, 0.4f, 0.4f);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, length, hitMask);

        if (hit)
        {
            OnHit(hit);
        }

        yield return new WaitForSeconds(0.3f);
        Disable();
    }

    private void OnHit(RaycastHit2D hit)
    {
        IReceiveAttack receiver;
        hit.collider.gameObject.TryGetComponent(out receiver);
        receiver.OnAttack(payload);
    }

    public override void SetInfo(Vector3 position, Vector3 direction, Player p, float len)
    {
        transform.position = position;
        dir = direction;
        length = len;
        payload.owner = p;
        isFree = false;

        payload.damage = 1; //p == Player.Player1 ? Global.GetP1Energy() : Global.GetP2Energy();

        LaserSettings();
        StartCoroutine(HitBox());
        //Invoke("Disable", 1f);
    }
    public override void Disable()
    {
        isFree = true;
        gameObject.SetActive(false);
        Destroy(circleStart);
        Destroy(circleEnd);
    }
}
