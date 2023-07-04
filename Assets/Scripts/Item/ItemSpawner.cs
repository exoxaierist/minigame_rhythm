using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    private List<Item> pool = new();

    [SerializeField] float spawnTime = 10f; 
    [SerializeField] List<Vector3> spawnPos = new List<Vector3>();

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

        return MakeNewItem(go);
    }

    private Item MakeNewItem(GameObject go)
    {
        Item item = Instantiate(go, transform).GetComponent<Item>();
        pool.Add(item);
        return item;
    }

    private Vector3 GetSpawnPos()
    {
        int range = spawnPos.Count;
        int randomPos = Random.Range(0, range);

        return spawnPos[randomPos];
    }
}
