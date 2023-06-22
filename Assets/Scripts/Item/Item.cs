using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    ItemType it;

    public void Init()
    {
        int itemCount = Enum.GetValues(typeof(ItemType)).Length;
        it = (ItemType)UnityEngine.Random.Range(0, itemCount);

        //юс╫ц
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        switch (it)
        {
            case ItemType.item1:
                sr.color = Color.red;
                break;
            case ItemType.item2:
                sr.color = Color.green;
                break;
            case ItemType.item3:
                sr.color = Color.blue;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IItemUser iu = collision.gameObject.GetComponentInParent<IItemUser>();
        if (iu == null)
            return;

        iu.UseItem(it);
        Destroy(gameObject);
    }
}
