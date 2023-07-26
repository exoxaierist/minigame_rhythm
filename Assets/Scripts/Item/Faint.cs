using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Faint : GridObject
{
    private Transform Target;
    public float speed = 1;
    private bool touch = false;

    [SerializeField]
    private short count;

    Coroutine faintCo;
    PlayerBase pb;

    public void Awake()
    {
        Global.OnBeat -= MoveTowardsTarget;
        Global.OnBeat += MoveTowardsTarget;
        Global.OnBeat -= ResetFunc;
        Global.OnBeat += ResetFunc;
    }

    public void setTarget(Player player)
    {
        string p = player == Player.Player1 ? "P2" : "P1";
        Target = GameObject.Find(p).transform;
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
        else if(relativeValue <= 0) //x와 y값이 같으면 relativeValue가 0이어서 안움직임
        {
            if (targetY > transform.position.y) MoveRelative(new Vector2(0, 1));
            else MoveRelative(new Vector2(0, -1));
        }
    }

    private void Count2Rhythm() => count++;

    private void OnTriggerStay2D(Collider2D col)
    {
        
        if (Target != null && col.gameObject == Target.gameObject)
        {
            Global.OnBeat -= Count2Rhythm;
            Global.OnBeat += MoveTowardsTarget;

            Global.OnBeat += Count2Rhythm;
            Global.OnBeat -= MoveTowardsTarget;

            GetComponent<BoxCollider2D>().enabled = false;
            faintCo = StartCoroutine(MakeTargetFaint());
        }
    }

    IEnumerator MakeTargetFaint()
    {
        count = 0;
        pb = Target.GetComponent<PlayerBase>();
        pb.fainted = true;
        transform.position = Target.position + new Vector3(0, 0.2f, 0);

        if (pb.player == Player.Player1)
            Global.P1AnyAction -= Global.energyManager.OnP1Any;
        else if (pb.player == Player.Player2)
            Global.P2AnyAction -= Global.energyManager.OnP2Any;

        while (true)
        {
            yield return null;
            if (count > 2)
                break;
        }

        if (pb.player == Player.Player1)
            Global.P1AnyAction += Global.energyManager.OnP1Any;
        else if (pb.player == Player.Player2)
            Global.P2AnyAction += Global.energyManager.OnP2Any;

        pb.fainted = false;
        Global.OnBeat -= Count2Rhythm;
        Global.OnReset -= ResetFunc;
        Destroy(gameObject);
    }

    private void ResetFunc()
    {
        if (faintCo == null || gameObject == null) return;

        StopCoroutine(faintCo);
        faintCo = null;
        Global.OnBeat -= MoveTowardsTarget;

        if (pb.player == Player.Player1)
            Global.P1AnyAction += Global.energyManager.OnP1Any;
        else if (pb.player == Player.Player2)
            Global.P2AnyAction += Global.energyManager.OnP2Any;

        pb.fainted = false;
        Global.OnBeat -= Count2Rhythm;
        Global.OnReset -= ResetFunc;
        Destroy(gameObject);
    }
}
