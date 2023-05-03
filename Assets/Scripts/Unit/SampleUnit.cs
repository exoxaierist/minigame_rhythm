using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleUnit : UnitBase
{
    private bool doubleTap = false;
    private string lastKey;
    private Coroutine upCoroutine;
    private Coroutine downCoroutine;
    private Coroutine rightCoroutine;
    private Coroutine leftCoroutine;
    private float timer;
    public Transform dir;
    protected override void Awake()
    {
        base.Awake();
    }

    
    protected override void MoveUp()
    {
        if (lastKey == "up" && doubleTap == true)
        {
            transform.DOPunchScale(new(0.2f, 0.2f, 0), 0.1f, 15);
            StopCoroutine(upCoroutine);
            doubleTap = false;
        }else
        {
            upCoroutine = StartCoroutine(Timer(() => base.MoveUp()));
            lastKey = "up";
            doubleTap = true;
        }
    }

    private IEnumerator Timer(Action _action)
    {
        timer = 0;
        while (timer < 0.2f)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        _action?.Invoke();
        doubleTap = false;
        timer = -1;
    }
}
