using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpCounterUI : MonoBehaviour
{
    public Player player;
    private Image spr;
    private Sprite emptySprite;
    private Sprite fullSprite;
    private Vector3 origin;
    private Color damageCol = new(1, 0, 0);
    private Color healCol = new(0, 1, 0);
    private Color emptyCol = new(0.2f, 0.2f, 0.2f);

    private void Start()
    {
        spr = GetComponent<Image>();
        emptySprite = Global.assets.hpCounterSpriteEmpty;
        if (player == Player.Player1) fullSprite = Global.assets.hpCounterSpriteFullP1;
        else fullSprite = Global.assets.hpCounterSpriteFullP2;
        SetFullSprite();
    }

    private void SetDefaultColor() => spr.color = Color.white;
    private void SetEmptyColor() => spr.color = emptyCol;
    private void SetDamageColor() => spr.color = damageCol;
    private void SetHealColor() => spr.color = healCol;

    public void SetEmptySprite()
    {
        spr.sprite = emptySprite;
    }
    public void SetEmpty() => SetEmpty(0);
    public void SetEmpty(float delay)
    {
        CancelInvoke(nameof(SetHealColor));
        CancelInvoke(nameof(SetDefaultColor));
        Invoke(nameof(SetDamageColor), delay);
        Invoke(nameof(SetEmptyColor), delay + 0.2f);
        transform.DOShakePosition(0.1f, new Vector3(0.07f, 0.07f, 0),30).OnComplete(() => ToOrigin()).SetDelay(delay);
        transform.DOShakeRotation(0.1f, new Vector3(0, 0, 20), 30).OnComplete(() => ToOrigin()).SetDelay(delay);
    }

    public void SetFullSprite()
    {
        spr.sprite = fullSprite;
    }
    public void SetFull() => SetFull(0);
    public void SetFull(float delay)
    {
        CancelInvoke(nameof(SetDamageColor));
        CancelInvoke(nameof(SetEmptyColor));
        Invoke(nameof(SetHealColor), delay);
        Invoke(nameof(SetDefaultColor), delay+0.2f);
        transform.DOPunchPosition(new Vector3(0, 0.1f, 0), 0.2f, 15).OnComplete(() => ToOrigin()).SetDelay(delay);
        transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0), 0.2f).OnComplete(() => ToOrigin()).SetDelay(delay);
    }

    public void SetOrigin()
    {
        origin = transform.localPosition;
    }

    private void ToOrigin()
    {
        transform.localPosition = origin;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;
    }

}
