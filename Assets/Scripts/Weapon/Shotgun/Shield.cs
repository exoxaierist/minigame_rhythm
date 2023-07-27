using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : WeaponType
{
    Hp hp;
    Vector3 posAdjust = new Vector3(0, 0, 1f);
    SpriteRenderer sp;

    private void LateUpdate()
    {
        transform.position = hp.transform.position + posAdjust;
    }

    public override void SetInfo(Vector3 position, Vector3 direction, Player p, float duration)
    {
        isFree = false;
        transform.position = position + posAdjust;
        payload.owner = p;

        transform.localScale = Vector3.zero;
        sp = GetComponent<SpriteRenderer>();
        if (sp != null)
            sp.color = new Color(1f, 1f, 0, 0.4f);

        transform.DOScale(Vector3.one* 1.5f, 0.05f);
    }

    public void OnDefence()
    {
        sp.transform.DOScale(3f, 0.05f).SetEase(Ease.OutExpo);
    }

    public void SetUser(Hp hp)
    {
        this.hp = hp;
    }

    public override void Disable()
    {
        isFree = true;
        gameObject.SetActive(false);
    }
}
