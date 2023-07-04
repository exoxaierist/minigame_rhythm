using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    ItemType it;

    public bool isFree = true;

    public void Init()
    {
        isFree = false;
        gameObject.SetActive(true);
        int itemCount = Enum.GetValues(typeof(ItemType)).Length;
        it = (ItemType)UnityEngine.Random.Range(1, itemCount);

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = Global.assets.itemImg[(int)it];

        Invoke(nameof(Disable), 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemFunc iFunc = collision.gameObject.GetComponent<ItemFunc>();
        if (iFunc == null)
            return;

        iFunc.StoreItem(it);

        Disable();
    }

    public void Disable()
    {
        isFree = true;
        gameObject.SetActive(false);
    }
}
