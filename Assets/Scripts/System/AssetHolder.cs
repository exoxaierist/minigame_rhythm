using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AssetCollector ������Ʈ�� Global�� ����
public class AssetHolder : MonoBehaviour
{
    public AssetCollector collector;

    private void Awake()
    {
        Global.assets = collector;
    }
}
