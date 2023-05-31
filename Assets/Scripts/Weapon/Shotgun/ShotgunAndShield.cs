using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ShotgunAndShield : Weapon
{
    Vector2 detectRange;
    LayerMask detectMask;

    Vector3 dir;
    Hp hp;

    private void Start()
    {
        detectRange = new Vector2(Global.gridIncrement * 7, Global.gridIncrement * 7);
        detectMask = player == Player.Player1 ? 1 << LayerMask.NameToLayer("P2") : 1 << LayerMask.NameToLayer("P1");
        dir = player == Player.Player1 ? Vector3.right : Vector3.left;
        hp = GetComponent<Hp>();
    }

    private void Update()
    {
        Collider2D enemy = Physics2D.OverlapBox(transform.position, detectRange, 0, detectMask);
        if (enemy == null)
            return;

        float angle = Quaternion.FromToRotation(transform.right, enemy.transform.position - transform.position).eulerAngles.z;
        dir = WeaponDir(angle);
        //Debug.Log(dir);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, detectRange);
    }

    public override void P1ShootForward() //shoot
    {
        Global.CamShakeLarge();
        Global.weaponPool.SpawnArms(Global.assets.slug, transform.position, dir, player);
    }

    public override void P1ShootVertical() //defense
    {
        hp.ShieldDeploy(2f);
    }

    public override void P2ShootForward() //shoot
    {
        Global.CamShakeLarge();
        Global.weaponPool.SpawnArms(Global.assets.slug, transform.position, dir, player);
    }

    public override void P2ShootVertical() //defense
    {
        hp.ShieldDeploy(2f);
    }

    private Vector3 WeaponDir(float angle)
    {
        if (angle <= 45) return Vector3.right;
        else if (angle <= 135) return Vector3.up;
        else if (angle <= 225) return Vector3.left;
        else if (angle <= 315) return Vector3.down;
        else return Vector3.right;
    }
}
