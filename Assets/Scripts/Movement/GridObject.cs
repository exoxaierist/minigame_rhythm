using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 그리드에 고정되며 움직이는 모든 오브젝트의 상위클래스
public class GridObject : EventObject
{
    [HideInInspector] public Vector2 offset = Vector2.zero;
    private float gridIncrement;

    protected bool isMoving = false;

    [Header("모션입힐 오브젝트")]
    public Transform visual;
    private Vector3 visualOffset;
    private Vector3 visualScale;

    private void Start()
    {
        gridIncrement = Global.gridIncrement;
        visualOffset = visual.localPosition;
        visualScale = visual.localScale;
        SnapToGrid();
    }

    // 상대적으로 이동
    public void MoveRelative(Vector2 dest)
    {
        if (isMoving) return;
        isMoving = true;
        transform.position = transform.position * Vector2.one + dest;
        if (visual != null)
        {
            visual.localPosition = -dest + Vector2.one*visualOffset ;
            visual.DORewind();
            visual.DOLocalJump(visualOffset, 0.6f, 1, 0.1f).SetEase(Ease.OutQuad).OnComplete(() => isMoving = false);
            visual.DOShakeScale(0.2f, new Vector3(0.2f, -0.2f, 0), 17).OnComplete(() => visual.localScale = visualScale);
        }
        else { isMoving = false; }
    }

    // 애니매이션 없이 이동
    public void Teleport(Vector3 dest)
    {
        transform.position = dest;
    }

    // 그리드에 스냅
    public void SnapToGrid() => transform.position = new Vector2(Mathf.Round(transform.position.x / gridIncrement), Mathf.Round(transform.position.y / gridIncrement)) + offset + Global.globalOffset;
}
