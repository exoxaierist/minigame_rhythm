using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Laser : Weapon
{
    [Header("최대 거리")]
    public float maxLen = 10;
    int maxRepeat = 2;
    private LayerMask mask;

    private void Start()
    {
        mask = 1 << LayerMask.NameToLayer("Portal") | 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("Reflect");
    }

    public override void P1ShootForward()
    {
        Calculate(Vector3.right, transform.position, maxLen);
    }
    public override void P1ShootVertical()
    {
        Calculate(Vector3.up, transform.position, maxLen);
        Calculate(Vector3.down, transform.position, maxLen);
    }
    public override void P2ShootForward()
    {
        Calculate(Vector3.left, transform.position, maxLen);
    }
    public override void P2ShootVertical()
    {
        Calculate(Vector3.up, transform.position, maxLen);
        Calculate(Vector3.down, transform.position, maxLen);
    }

    private void Calculate(Vector3 dir, Vector3 pos, float len, int repeat = 0)
    {
        float distance = len;
        RaycastHit2D hit = Physics2D.Raycast(pos + dir, dir, len, mask);
        if (hit.collider != null)
        {
            distance = Vector2.Distance(pos + dir, hit.point);
            //Debug.Log(hit.collider.name + " " + distance);

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
                    Calculate(RotateVector(rotationDir * -dir, p.RotationValue), p.transform.position, len - distance, repeat + 1);
                }            
            }

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Reflect"))
            {
                Vector3 flectDir = Vector3.Reflect(dir, hit.normal);
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
        Global.weaponPool.SpawnArms(Global.assets.laser, pos, dir, player, len);
    }

    //레이저 위치 및 거리 계산
    //private void CalculateLaser(Vector3 dir)
    //{     
          //dangerLine.SetPosition(1, dir * distance + dir);
    //    //laser.SetPosition(1, dir * distance + dir);
    //    //dangerLine.startWidth = 0;
    //    //dangerLine.endWidth = 0;
    //    //DOTween.To(() => dangerLine.startWidth, x => dangerLine.startWidth = x, 0.5f, 1).SetEase(Ease.OutQuint);
    //    //DOTween.To(() => dangerLine.endWidth, x => dangerLine.endWidth = x, 0.5f, 1).SetEase(Ease.OutQuint);
    //    //Sequence sequence = DOTween.Sequence();
    //    //sequence.Append(dangerLine.DOColor(new Color2(Color.red, Color.red), new Color2(Color.white, Color.white), 1));
    //    //sequence.InsertCallback(1,() => { dangerLine.startColor = Color.blue; dangerLine.endColor = Color.blue; });
    //    //sequence.Append(dangerLine.DOColor(new Color2(Color.red,Color.red),new Color2(Color.blue,Color.blue),0.2f).SetDelay(0.1f));
    //}

}
