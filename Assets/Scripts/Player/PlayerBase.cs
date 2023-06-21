using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���� ���
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

    // ������Ʈ�� ���� �����ִ� HP ������Ʈ ã�Ƽ� �̺�Ʈ ���
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
