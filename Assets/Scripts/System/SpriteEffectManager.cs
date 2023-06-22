using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteEffectManager : MonoBehaviour
{
    private GameObject poolParent;
    private readonly List<SpriteEffect> pool = new();

    private void Awake()
    {
        Global.sprEffectManager = this;
    }

    private SpriteEffect GetClone() 
    {
        foreach (SpriteEffect effect in pool) if (effect.isFree) return effect;
        print("asdf");
        MakePool(1);
        foreach (SpriteEffect effect in pool) if (effect.isFree) return effect;
        return null;
    }

    private void MakePool(int size)
    {
        SetPoolParent();
        for (int i = 0; i < size; i++)
        {
            SpriteEffect instance = Instantiate(Global.assets.spriteEffect,poolParent.transform).GetComponent<SpriteEffect>();
            instance.origin = poolParent.transform;
            pool.Add(instance);
        }
    }

    private void SetPoolParent()
    {
        if (poolParent != null) return;
        poolParent = new();
        poolParent.name = "SpriteEffectPool";
        poolParent.transform.SetParent(transform);
    }

    public void SpawnEffect(string name, Vector3 position, float rotation) => SpawnEffect(name, position, rotation, null);
    public void SpawnEffect(string name, Vector3 position, float rotation,Transform parent)
    {
        SpriteEffect instance = GetClone();
        if (parent != null) instance.transform.SetParent(parent);
        instance.enabled=true;
        instance.transform.SetPositionAndRotation(position, Quaternion.Euler(0, 0, rotation));
        
        instance.Effect(name);
    }
}
