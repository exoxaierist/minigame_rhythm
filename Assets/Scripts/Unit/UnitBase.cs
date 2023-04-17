using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���� ���
public class UnitBase : ControlledObject
{
    [Header("���� ����")]
    public string id = "unassigned";
    [HideInInspector] public Hp hp;
    public PerkBase perk;

    protected override void Awake()
    {
        base.Awake();
        CheckForHP();
    }

    protected virtual void OnHeal() { }
    protected virtual void OnDamage() { }
    protected virtual void OnDeath() { }

    // ������Ʈ�� ���� �����ִ� HP ������Ʈ ã�Ƽ� �̺�Ʈ ���
    protected void CheckForHP()
    {
        if (TryGetComponent<Hp>(out hp))
        {
            hp.OnHeal += OnHeal;
            hp.OnDamage += OnDamage;
            hp.OnDeath += OnDeath;
        }
    }
}
