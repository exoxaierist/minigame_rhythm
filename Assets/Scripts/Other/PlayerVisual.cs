using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private SpriteRenderer spr;
    private PlayerBase owner;
    private Laser laser;
    private int prevEnergy;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        owner = transform.parent.GetComponent<PlayerBase>();
        laser = owner.GetComponent<Laser>();
        if (owner.player == Player.Player1) { spr.color = Global.p1Color; Global.OnP1MissBeat += DamageEffect; }
        else { spr.color = Global.p2Color; Global.OnP2MissBeat += DamageEffect; }
    }

    private void Start()
    {
        owner.hp.OnDamage += DamageEffect;
    }

    public void DamageEffect()
    {
        spr.DOComplete();
        Color apply;
        if (owner.player == Player.Player1) apply = Global.p1Color;
        else apply = Global.p2Color;
        spr.color = Global.whiteColor;
        spr.DOColor(apply, 0.05f).SetDelay(0.1f);
    }
}
