using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTest : MonoBehaviour
{
    Weapon w;
    private void Start()
    {
        w = GetComponentInChildren<Weapon>();
        if (w == null)
            Debug.Log("null");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            w?.IncRhythmEnergy();

        if (Input.GetKeyDown(KeyCode.F))
            w?.Shoot(KeyCode.F);
        else if(Input.GetKeyDown(KeyCode.G))
            w?.Shoot(KeyCode.G);

    }
}
