using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Hp : MonoBehaviour, IReceiveAttack
{
    [Header("UI")]
    public bool showHpUI = true;
    public Vector2 hpUIOffset = new(0, 0.8f);
    public bool autoParent = true;
    private Transform autoParentTransform;
    public Transform customUIParent;
    public HpUIType hpUIType;
    private HpUI hpUI;

    [Header("HP")]
    [SerializeField] protected int maxHp = 10;
    [SerializeField] protected int hp;

    // �ǰ�, ��� �븮��
    public Action OnDamage;
    public Action OnHeal;
    public Action OnHpChange;
    public Action OnDeath;

    private PlayerBase ownerPlayer;
    private bool isPlayer = false;

    //���� ����
    public bool isProtected = false;
    private WeaponType shield;

    public void OnAttack(AttackInfo info)
    {
        if (isPlayer && info.owner != ownerPlayer.player) AddToHP(-info.damage);
    }

    private void Awake()
    {
        isPlayer = TryGetComponent(out ownerPlayer);
    }

    private void Start()
    {
        hp = maxHp;
        if (autoParent && TryGetComponent(out GridObject gridobj)) autoParentTransform = gridobj.visual;
        if (showHpUI) hpUI = CreateHpBar();
    }

    // ü�� �����Ҷ� ���
    public void AddToHP(int value)
    {
        if (value == 0) return;
        hp = Mathf.Clamp(hp + value, 0, maxHp);
        hpUI.SetHP(hp);

        // �븮�� ȣ��
        if (value > 0) OnHeal?.Invoke();
        else OnDamage?.Invoke();
        CheckDeath();
    }

    // �׾����� Ȯ��
    private void CheckDeath()
    {
        if (hp <= 0) Death();
    }

    // ����
    private void Death()
    {
        hp = 0;
        OnDeath?.Invoke();
    }

    // HP UI������Ʈ ����
    private HpUI CreateHpBar()
    {
        GameObject instance = Instantiate(Global.assets.hpUI, autoParent?autoParentTransform:customUIParent);
        instance.transform.localPosition = hpUIOffset;
        instance.GetComponent<HpUI>().Set(maxHp,hp,hpUIType);
        return instance.GetComponent<HpUI>();
    }

    //����
    public void ShieldDeploy(float duration)
    {
        if (isProtected)
            return;

        isProtected = true;
        shield = Global.weaponPool.SpawnArms(Global.assets.shield, transform.position, Vector3.zero, ownerPlayer.player);

        Invoke("ShieldUnDeploy", duration);
    }

    public void ShieldUnDeploy()
    {
        isProtected = false;
        shield.Disable();
    }
}
