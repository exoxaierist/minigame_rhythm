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

    // ????, ???? ??????
    public Action OnDamage;
    public Action OnHeal;
    public Action OnHpChange;
    public Action OnDeath;

    private PlayerBase ownerPlayer;
    private bool isPlayer = false;

    //???? ????
    public bool isProtected = false;
    private WeaponType shield;

    //Item
    private ItemFunc itemFunc;

    public void OnAttack(AttackInfo info)
    {
        if (isProtected)
        {
            ShieldUnDeploy();

            if (ownerPlayer.player == Player.Player1) Global.energyManager.IncP1Energy();
            else if(ownerPlayer.player == Player.Player2) Global.energyManager.IncP2Energy();

            return;
        }

        if (isPlayer && info.owner != ownerPlayer.player) AddToHP(-info.damage);
    }

    private void Awake()
    {
        isPlayer = TryGetComponent(out ownerPlayer);
        itemFunc = GetComponent<ItemFunc>();
    }

    private void Start()
    {
        hp = maxHp;
        if (autoParent && TryGetComponent(out GridObject gridobj)) autoParentTransform = gridobj.visual;
        if (showHpUI) hpUI = CreateHpBar();
    }

    // ???? ???????? ????
    public void AddToHP(int value)
    {
        if (value == 0) return;
        hp = Mathf.Clamp(hp + value, 0, maxHp);
        hpUI.SetHP(hp);

        // ?????? ????
        if (value > 0) OnHeal?.Invoke();
        else
        {
            Global.sfx.Play(Global.assets.aHitDamage);
            OnDamage?.Invoke();
        }
        CheckDeath();
    }

    // ???????? ????
    private void CheckDeath()
    {
        if (hp <= 0) Death();
    }

    // ????
    private void Death()
    {
        hp = 0;
        
        OnDeath?.Invoke();
    }
    public void HpReturn()
    {
        hp = maxHp;
    }
    // HP UI???????? ????
    private HpUI CreateHpBar()
    {
        GameObject instance = Instantiate(Global.assets.hpUI, autoParent?autoParentTransform:customUIParent);
        instance.transform.localPosition = hpUIOffset;
        instance.GetComponent<HpUI>().Set(maxHp,hp,hpUIType,ownerPlayer.player);
        return instance.GetComponent<HpUI>();
    }

    //????
    public void ShieldDeploy(float duration)
    {
        if (isProtected)
            return;

        if (ownerPlayer.player == Player.Player1) Global.energyManager.DecP1Energy();
        else if (ownerPlayer.player == Player.Player2) Global.energyManager.DecP2Energy();
        isProtected = true;
        shield = Global.weaponPool.SpawnArms(Global.assets.shield, transform.position, Vector3.zero, ownerPlayer.player);
        Shield s = shield as Shield;
        s.SetUser(this);

        Invoke(nameof(ShieldUnDeploy), duration);
    }

    public void ShieldUnDeploy()
    {
        isProtected = false;
        if (shield == null)
            return;

        shield.Disable();
        shield = null;
    }
}
