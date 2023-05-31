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

    // 피격, 사망 대리자
    public Action OnDamage;
    public Action OnHeal;
    public Action OnHpChange;
    public Action OnDeath;

    private PlayerBase ownerPlayer;
    private bool isPlayer = false;

    //쉴드 관련
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

    // 체력 변경할때 사용
    public void AddToHP(int value)
    {
        if (value == 0) return;
        hp = Mathf.Clamp(hp + value, 0, maxHp);
        hpUI.SetHP(hp);

        // 대리자 호출
        if (value > 0) OnHeal?.Invoke();
        else OnDamage?.Invoke();
        CheckDeath();
    }

    // 죽었는지 확인
    private void CheckDeath()
    {
        if (hp <= 0) Death();
    }

    // 죽임
    private void Death()
    {
        hp = 0;
        OnDeath?.Invoke();
    }

    // HP UI오브젝트 생성
    private HpUI CreateHpBar()
    {
        GameObject instance = Instantiate(Global.assets.hpUI, autoParent?autoParentTransform:customUIParent);
        instance.transform.localPosition = hpUIOffset;
        instance.GetComponent<HpUI>().Set(maxHp,hp,hpUIType);
        return instance.GetComponent<HpUI>();
    }

    //쉴드
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
