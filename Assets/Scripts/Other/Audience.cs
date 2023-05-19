using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audience : MonoBehaviour
{
    private Vector3 origin;
    public Sprite spr1;
    public Sprite spr2;
    public Sprite spr3;
    private SpriteRenderer spr;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        origin = transform.position;
        Global.OnBeat += Hop;
    }

    private void Start()
    {
        float rand = (float)Random.Range(0, 100) / 100;
        spr.sprite = (rand > 0.66f) ? spr1 : (rand < 0.33f) ? spr2 : spr3;
    }

    private void Hop()
    {
        transform.DORewind();
        transform.DOJump(origin, 0.2f, 1, 0.15f).SetDelay(Random.Range(0, 0.2f));
    }
}
