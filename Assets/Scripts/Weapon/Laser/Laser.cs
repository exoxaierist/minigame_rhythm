using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Laser : Weapon
{
    public AttackInfo payload;

    [Header("�ִ� �Ÿ�")]
    public float maxLen = 10;

    private LayerMask mask;

    private void Start()
    {
        mask = 1 << LayerMask.NameToLayer("Portal") | 1 << LayerMask.NameToLayer("Wall");
    }

    public override void P1ShootForward()
    {
        Calculate(Vector3.right, transform.position, maxLen);
    }
    public override void P1ShootUpDown()
    {
        Calculate(Vector3.up, transform.position, maxLen);
        Calculate(Vector3.down, transform.position, maxLen);
    }
    public override void P2ShootForward()
    {
        Calculate(Vector3.left, transform.position, maxLen);
    }
    public override void P2ShootUpDown()
    {
        Calculate(Vector3.up, transform.position, maxLen);
        Calculate(Vector3.down, transform.position, maxLen);
    }

    private void Calculate(Vector3 dir, Vector3 pos, float len)
    {
        float distance = len;
        RaycastHit2D hit = Physics2D.Raycast(pos + dir, dir, len, mask);
        if (hit.collider != null)
        {
            distance = Vector2.Distance(pos + dir, hit.point);
            //Debug.Log(hit.collider.name + " " + distance);

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Portal"))
            {
                Portal p = hit.collider.GetComponent<Portal>();
                Calculate(dir, p.destination.position, len - distance);
            }
        }

        CreateLaser(dir, pos, distance);
    }

    private void CreateLaser(Vector3 dir, Vector3 pos, float len)
    {
        Global.weaponPool.SpawnArms(Global.assets.laser, pos, dir, player, len);
    }

    //������ ��ġ �� �Ÿ� ���
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
