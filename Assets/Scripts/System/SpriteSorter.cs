using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// y��ǥ������ ��������Ʈ ���� �������ִ� ��ũ��Ʈ
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
