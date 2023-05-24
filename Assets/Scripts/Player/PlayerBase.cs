using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 유닛 개발 기반
public class PlayerBase : ControlledObject
{
    [HideInInspector] public Hp hp;

    protected override void Awake()
    {
        base.Awake();
        CheckForHP();
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
        if (Global.CheckBeat() && Global.IsInField(transform.position*Vector2.one + target) && !Global.CheckOverlap(transform.position * Vector2.one + target, collisionLayer.value)) MoveRelative(target);
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
