using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Faint : MonoBehaviour
{
    private Transform Target;
    public float speed = 1;

    private void Awake()
    {
        Global.OnBeat += MoveTowardsTarget;
    }

    public void setTarget(int target)
    {
        this.Target = GameObject.Find("P" + target.ToString()).transform;
    }

    private void MoveTowardsTarget()
    {
        if (Target == null) return;
        Vector2 targetPosition = Target.position;
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        Vector2 moveDistance = direction * speed;

        transform.DOMove((Vector2)transform.position + moveDistance, 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (Target != null && col.gameObject == Target.gameObject)
        {
            Global.OnBeat -= MoveTowardsTarget;
            col.GetComponent<Hp>().AddToHP(-2);
            Destroy(this.gameObject);
        }
    }
}
