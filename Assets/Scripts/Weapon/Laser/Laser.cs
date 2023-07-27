using DG.Tweening;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class Laser : Weapon
{
    public float maxLen = 1000;
    int maxRepeat = 2;
    private LayerMask mask;
    public Color col;

    private void Start()
    {
        mask = 1 << LayerMask.NameToLayer("Portal") | 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("Reflect");
    }

    public override void P1ShootForward()
    {
        if (!Global.CheckBeat()) { Global.OnP2MissBeat?.Invoke(); return; }
        if (Global.energyManager.GetP1Energy() <= 0) return;
        if (owner.actionCount > 0) return;
        else owner.actionCount++;
        Global.energyManager.DecP1Energy();

        Calculate(Vector3.right, transform.position, maxLen);
        Calculate(Vector3.left, transform.position, maxLen);
    }
    public override void P1ShootVertical()
    {
        if (!Global.CheckBeat()) { Global.OnP2MissBeat?.Invoke(); return; }
        if (Global.energyManager.GetP1Energy() <= 0) return;
        if (owner.actionCount > 0) return;
        else owner.actionCount++; 
        Global.energyManager.DecP1Energy();

        Calculate(Vector3.up, transform.position, maxLen);
        Calculate(Vector3.down, transform.position, maxLen);
    }
    public override void P2ShootForward()
    {
        if (!Global.CheckBeat()) { Global.OnP2MissBeat?.Invoke(); return; }
        if (Global.energyManager.GetP2Energy() <= 0) return;
        if (owner.actionCount > 0) return;
        else owner.actionCount++; 
        Global.energyManager.DecP2Energy();


        Calculate(Vector3.left, transform.position, maxLen);
        Calculate(Vector3.right, transform.position, maxLen);
    }
    public override void P2ShootVertical()
    {
        if (!Global.CheckBeat()) { Global.OnP2MissBeat?.Invoke(); return; }
        if (Global.energyManager.GetP2Energy() <= 0) return;
        if (owner.actionCount > 0) return;
        else owner.actionCount++; 
        Global.energyManager.DecP2Energy();

        Calculate(Vector3.up, transform.position, maxLen);
        Calculate(Vector3.down, transform.position, maxLen);
    }

    private void Calculate(Vector3 dir, Vector3 pos, float len, int repeat = 0)
    {
        float distance = len;
        RaycastHit2D hit = Physics2D.Raycast(pos, dir, len, mask);
        if (hit.collider != null)
        {
            distance = Vector2.Distance(pos, hit.point);

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Portal") && repeat < maxRepeat)
            {
                Portal p = hit.collider.GetComponent<Portal>().destination.GetComponent<Portal>();               
                if (p.isLocking)
                {                
                    Calculate(RotateVector(p.Direction, p.RotationValue), p.transform.position, len - distance, repeat + 1);
                    Calculate(RotateVector(-p.Direction, p.RotationValue), p.transform.position, len - distance, repeat + 1);
                }                  
                else
                {
                    Quaternion rotationDir = p.transform.rotation;
                    Calculate(RotateVector(rotationDir * dir, p.RotationValue), p.transform.position, len - distance, repeat+1);
                    //Calculate(RotateVector(rotationDir * -dir, p.RotationValue), p.transform.position, len - distance, repeat + 1);
                }
            }

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Reflect"))
            {
                Vector3 flectDir = hit.transform.TransformDirection(Vector3.up);
                Calculate(flectDir, hit.collider.transform.position, len - distance);
            }
        }

        CreateLaser(dir, pos, distance);
    }

    Vector3 RotateVector(Vector3 vector, float angle)
    {
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        return rotation * vector;
    }

    private void CreateLaser(Vector3 dir, Vector3 pos, float len)
    {
        WeaponType instance = Global.weaponPool.SpawnArms(Global.assets.laser, pos, dir, player, len);
        if(instance is Beam && pos == transform.position)
        {
            (instance as Beam).LaserStart();
        }
    }

    public void CrossLaser()
    {
        Vector3 direction = Quaternion.AngleAxis(45, Vector3.forward) * Vector3.up;

        for (int i = 0; i < 4; i++)
        {
            direction = Quaternion.AngleAxis(90, Vector3.forward) * direction;
            Calculate(direction, transform.position, maxLen);
        }
    }

}
