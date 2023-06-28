using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    private List<Item> pool = new();

    [SerializeField] float spawnTime;

    private void Start()
    {
        Global.itemSpawner = this;

        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);

            Item i = GetClone(Global.assets.item);
            i.transform.position = GetSpawnPos();
            i.Init();
        }
    }

    private Item GetClone(GameObject go)
    {
        foreach (Item item in pool)
            if (item.isFree)
                return item;

        return MakeNewWeapon(go);
    }

    private Item MakeNewWeapon(GameObject go)
    {
        Item item = Instantiate(go, transform).GetComponent<Item>();
        pool.Add(item);
        return item;
    }

    private Vector3 GetSpawnPos()
    {
        //일단 가운데 출몰
        Vector3 v = Global.fieldExtent/2;

        return v;
    }
}
