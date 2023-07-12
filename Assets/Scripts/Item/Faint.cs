using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Faint : GridObject
{
    private Transform Target;
    public float speed = 1;
    private bool touch = false;
    public void Awake()
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
        float targetX = Target.transform.position.x;
        float targetY = Target.transform.position.y;
        float relativeValue = Mathf.Abs(targetX - transform.position.x) - Mathf.Abs(targetY - transform.position.y);
        if(relativeValue > 0)
        {
            if (targetX > transform.position.x) MoveRelative(new Vector2(1, 0));
            else MoveRelative(new Vector2(-1, 0));
        }
        else if(relativeValue < 0)
        {
            if (targetY > transform.position.y) MoveRelative(new Vector2(0, 1));
            else MoveRelative(new Vector2(0, -1));
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        
        if (Target != null && col.gameObject == Target.gameObject)
        {
            Global.OnBeat -= MoveTowardsTarget;
            col.GetComponent<Hp>().AddToHP(-2);
            Destroy(this.gameObject);
        }
    }
}
