using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    private List<Item> pool = new();

    [SerializeField] float spawnTime = 10f;
    [SerializeField] Transform spawnObj = null;
    [SerializeField] List<Transform> spawnPos = new List<Transform>();

    Coroutine spawnCo;

    private void Start()
    {
        Global.itemSpawner = this;
        Global.OnReset += ResetFunc;

        if(spawnObj == null)
            spawnObj = transform.GetChild(0);
        for (int i = 0; i < spawnObj.childCount; i++)
            spawnPos.Add(spawnObj.GetChild(i));

        //Debug.Log(spawnPos.Count);
        spawnCo = StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);

            while(spawnPos.Any(x => x.gameObject.activeSelf == false))
                yield return new WaitForSeconds(spawnTime);

            Item i = GetClone(Global.assets.item);

            Transform pos = GetSpawnPos();
            if (pos.gameObject.activeSelf)
            {
                i.Init(pos);
                pos.gameObject.SetActive(false);
            }
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
        if (spawnCo == null)
            return;

        StopCoroutine(spawnCo);
        spawnCo = null;
        foreach (Item item in pool)
        {
            if(!item.isFree)
                item.Disable();
        }

        spawnCo = StartCoroutine(Spawn());
    }
}
