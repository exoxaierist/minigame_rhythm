using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject item;
    float spawnTime;

    private void Start()
    {
        spawnTime = 10f;
        //StartCoroutine(Spawn());
        Item i = Instantiate(item, transform).GetComponent<Item>();
        i.Init();
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            Item i = Instantiate(item, transform).GetComponent<Item>();
            i.Init();
        }
    }
}
