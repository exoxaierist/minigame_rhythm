using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    ItemType it;

    [SerializeField]
    float disabledTime = 5.0f;

    Transform posObj;
    public bool isFree = true;

    public void Init(Transform pos)
    {
        posObj = pos;
        posObj.gameObject.SetActive(false);

        transform.position = pos.position;
        isFree = false;
        gameObject.SetActive(true);
        int itemCount = Enum.GetValues(typeof(ItemType)).Length;
        it = (ItemType)UnityEngine.Random.Range(1, itemCount);

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = Global.assets.itemImg[(int)it];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemFunc iFunc = collision.gameObject.GetComponent<ItemFunc>();
        if (iFunc == null)
        {
            Debug.Log("Nul");
            return;
        }

        Global.sfx.Play(Global.assets.aGetItem);
        iFunc.StoreItem(it);

        Disable();
    }

    public void Disable()
    {
        isFree = true;
        posObj.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
