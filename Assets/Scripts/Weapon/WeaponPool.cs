using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPool : MonoBehaviour
{
    //private GameObject poolParent;
    private List<WeaponType> pool = new();

    private void Awake()
    {
        Global.weaponPool = this;
    }

    private WeaponType GetClone(GameObject go)
    {
        foreach (WeaponType w in pool) 
            if (w.isFree && w.wtype == go.GetComponent<WeaponType>().wtype) 
                return w;

        return MakeNewWeapon(go);
    }

    private WeaponType MakeNewWeapon(GameObject go)
    {
        WeaponType arms = Instantiate(go, transform).GetComponent<WeaponType>();
        pool.Add(arms);
        return arms;
    }

    public void SpawnArms(GameObject go, Vector3 position, Vector3 dir, Player p, float speed) => SpawnArms(go, position, dir, p, speed, transform);
    public void SpawnArms(GameObject go, Vector3 position, Vector3 dir, Player p) => SpawnArms(go, position, dir, p, 0, transform);
    public void SpawnArms(GameObject go, Vector3 position, Vector3 dir, Player p, float len, Transform parent)
    {
        WeaponType instance = GetClone(go);
        instance.transform.SetParent(parent);
        instance.gameObject.SetActive(true);
        instance.SetInfo(position, dir, p, len);
    }
}
