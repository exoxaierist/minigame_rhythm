using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerChanger : MonoBehaviour
{

    SpriteRenderer spren;
    // Start is called before the first frame update
    void Awake()
    {
        spren = GetComponent<SpriteRenderer>();
        spren.sortingLayerName = "Background";
    }

    public void LayerToBackground()
    {
        spren.sortingLayerName = "Background";
    }
    public void LayerToDefault()
    {
        spren.sortingLayerName = "WallTop";
    }
}
