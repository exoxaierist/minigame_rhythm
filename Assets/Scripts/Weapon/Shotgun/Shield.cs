using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : WeaponType
{
    Hp hp;

    private void Update()
    {
        transform.position = hp.transform.position;
    }

    public override void SetInfo(Vector3 position, Vector3 direction, Player p, float duration)
    {
        isFree = false;
        transform.position = position;
        payload.owner = p;

        transform.localScale = Vector3.zero;
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        if (sp != null)
            sp.color = new Color(0, 0.2f, 1, 0.5f);

        transform.DOScale(Vector3.one, 0.05f);
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
