using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 유닛 개발 기반
public class PlayerBase : ControlledObject
{
    [HideInInspector] public Hp hp;

    public bool fainted = false;

    protected override void Awake()
    {
        base.Awake();
        CheckForHP();
        if (player == Player.Player1) collisionLayer = Global.p1MoveColMask;
        else collisionLayer = Global.p2MoveColMask;
        Global.OnReset += () => { fainted = false; };
    }

    protected virtual void OnHeal() { }
    protected virtual void OnDamage() { }
    protected virtual void OnDeath() { }

    protected override void MoveUp()
    {
        Move(new(0, 1));
    }

    protected override void MoveDown()
    {
        Move(new(0, -1));
    }

    protected override void MoveRight()
    {
        Move(new(1, 0));
    }

    protected override void MoveLeft()
    {
        Move(new(-1,0));
    }

    private void Move(Vector2 target)
    {
        if (fainted)
            return;

        if (Global.CheckBeat() && !Global.CheckOverlap(transform.position * Vector2.one + target, collisionLayer)) MoveRelative(target);
        else
        {
            if (player == Player.Player1) Global.energyManager.DecP1Energy();
            else Global.energyManager.DecP2Energy();
        }
    }

    // 오브젝트에 같이 딸려있는 HP 컴포넌트 찾아서 이벤트 등록
    protected void CheckForHP()
    {
        if (TryGetComponent(out hp))
        {
            hp.OnHeal += OnHeal;
            hp.OnDamage += OnDamage;
            hp.OnDeath += OnDeath;
        }
    }
}
