using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    private List<Item> pool = new();

    [SerializeField] Transform spawnObj = null;
    [SerializeField] List<Transform> spawnPos = new List<Transform>();
    Transform spawnedPos;

    private void Start()
    {
        Global.itemSpawner = this;
        Global.OnReset += ResetFunc;

        if(spawnObj == null)
            spawnObj = transform.GetChild(0);
        for (int i = 0; i < spawnObj.childCount; i++)
            spawnPos.Add(spawnObj.GetChild(i));

        Spawn();
    }

    public void Spawn()
    {
        Item i = GetClone(Global.assets.item);

        Transform pos = GetSpawnPos();
        while (pos == spawnedPos)
        {
            pos = GetSpawnPos();
            //Debug.Log("samePos");
        }

        spawnedPos = pos;
        if (spawnedPos.gameObject.activeSelf)
        {
            i.Init(spawnedPos);
            spawnedPos.gameObject.SetActive(false);
        }
    }

    private Item GetClone(GameObject go)
    {
        foreach (Item item in pool)
            if (item.isFree)
                return item;

        return MakeNewItem(go);
    }

    private Item MakeNewItem(GameObject go)
    {
        Item item = Instantiate(go, transform).GetComponent<Item>();
        pool.Add(item);
        return item;
    }

    private Transform GetSpawnPos()
    {
        int range = spawnPos.Count;
        int randomPos = Random.Range(0, range);

        return spawnPos[randomPos];
    }

    private void ResetFunc()
    {
        foreach (Item item in pool)
        {
            if(!item.isFree)
                item.Disable();
        }

        Spawn();
    }
}
