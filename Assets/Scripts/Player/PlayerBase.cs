using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ???? ???? ????
public class PlayerBase : ControlledObject
{
    [HideInInspector] public Hp hp;

    public bool fainted = false;
    readonly List<string> movemem = new();

    public int actionCount = 0;

    protected override void Awake()
    {
        base.Awake();
        movemem.Add("start");
        CheckForHP();
        if (player == Player.Player1) collisionLayer = Global.p1MoveColMask;
        else collisionLayer = Global.p2MoveColMask;

        Global.OnReset -= ResetFunc;
        Global.OnReset += ResetFunc;
        Global.OnLastTiming += CheckDoNothing;
    }

    protected virtual void OnHeal() { }
    protected virtual void OnDamage() { }
    protected virtual void OnDeath() { }

    protected override void MoveUp()
    {
        if (!Global.CheckBeat() && actionCount > 0) return;
        actionCount++;
        Move(new(0, 1));
        movemem.RemoveAt(0);
        movemem.Add("MoveUp");
    }

    protected override void MoveDown()
    {
        if (!Global.CheckBeat() && actionCount > 0) return;
        actionCount++;
        Move(new(0, -1));
        movemem.RemoveAt(0);
        movemem.Add("MoveDown");
    }

    protected override void MoveRight()
    {
        if (!Global.CheckBeat() && actionCount > 0) return;
        actionCount++;
        Move(new(1, 0));
        movemem.RemoveAt(0);
        movemem.Add("MoveRight");
    }

    protected override void MoveLeft()
    {
        if (!Global.CheckBeat() && actionCount > 0) return;
        actionCount++;
        Move(new(-1,0));
        movemem.RemoveAt(0);
        movemem.Add("MoveLeft");
    }

    private void Move(Vector2 target)
    {
        if (fainted)
            return;

        if (Global.CheckBeat() && !Global.CheckOverlap(transform.position * Vector2.one + target, collisionLayer)) MoveRelative(target);
        else
        {
            if (player == Player.Player1) { Global.energyManager.DecP1Energy(); Global.OnP1MissBeat?.Invoke(); }
            else { Global.energyManager.DecP2Energy(); Global.OnP2MissBeat?.Invoke(); }
        }
    }

    // ?????????? ???? ???????? HP ???????? ?????? ?????? ????
    protected void CheckForHP()
    {
        if (TryGetComponent(out hp))
        {
            hp.OnHeal += OnHeal;
            hp.OnDamage += OnDamage;
            hp.OnDeath += OnDeath;
        }
    }

    private void ResetFunc()
    {
        fainted = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            switch (movemem[0])
            {
                case "MoveUp":
                    MoveDown();
                    break;
                case "MoveDown":
                    MoveUp();
                    break;
                case "MoveRight":
                    MoveLeft();
                    break;
                case "MoveLeft":
                    MoveRight();
                    break;
            }
        }
    }

    private void CheckDoNothing()
    {
        if(actionCount == 0)
        {
            if (player == Player.Player1) Global.energyManager.DecP1Energy();
            else Global.energyManager.DecP2Energy();
        }
        actionCount = 0;
    }
}
