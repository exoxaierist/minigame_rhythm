using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// y좌표에따라 스프라이트 순서 세팅해주는 스크립트
public class SpriteSorter : MonoBehaviour
{
    SpriteRenderer spr;

    private void Start()
    {
        if (!TryGetComponent(out spr)) enabled = false;
    }

    private void Update()
    {
        Sort();
    }

    private void Sort()
    {
        spr.sortingOrder = (int)((transform.position.y + 50)*5);
    }
}
