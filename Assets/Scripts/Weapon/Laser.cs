using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Laser : Weapon
{
    [SerializeField]
    LineRenderer laser;
    [SerializeField]
    Transform pool;

    [Header("최대 거리")]
    public float maxLen = 10;

    private LayerMask mask;

    //임시 효과 - 색
    Color c = Color.red;

    private void Start()
    {
        mask = 1 << LayerMask.NameToLayer("Portal") | 1 << LayerMask.NameToLayer("Wall");
    }
    private void Update()
    {
    }

    public override void P1ShootForward()
    {
    }
    public override void P1ShootUpDown()
    {
    }
    public override void P2ShootForward()
    {
    }
    public override void P2ShootUpDown()
    {
    }

    public override void ShootFail()
    {
    }

    //임시
    private void LaserEffectByLevel(RhythmLevel rl)
    {
        switch (rl)
        {
            case RhythmLevel.One:
                damage = 1;
                c = Color.red;
                break;
            case RhythmLevel.Two:
                damage = 2;
                c = Color.green;
                break;
            case RhythmLevel.Three:
                damage = 3;
                c = Color.blue;
                break;
        }
    }

    //임시
    IEnumerator ShootFunc()
    {
        //pos = transform.position;

        //dangerLine.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        //dangerLine.gameObject.SetActive(false);

        laser.startColor = c;
        laser.endColor = c;
        laser.enabled = true;
        yield return new WaitForSeconds(0.3f);
        laser.enabled = false;

        transform.position = transform.parent.position;
    }

    private void Calculate(Vector3 dir, Vector2 pos, float len, Player player)
    {
        float distance = len;
        RaycastHit2D hit = Physics2D.Raycast(transform.position/*플레이어 위치*/ + dir, dir, len, mask);
        if (hit.collider != null)
        {
            distance = Vector2.Distance(transform.position + dir, hit.point);
            //Debug.Log(hit.collider.name + " " + distance);

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Portal"))
            {
                //TODO
                //Portal p = hit.collider.GetComponent<Portal>();
                //포탈내의 정보 추출 후, 포탈 위치에서 재 생성
                //Calculate()
            }
        }

        CreateLaser(dir, pos, distance);
    }

    private void CreateLaser(Vector3 dir, Vector2 pos, float len)
    {
        LineRenderer beam = Instantiate(laser, pos, Quaternion.identity, pool);
        beam.transform.position = pos;
        beam.SetPosition(0, dir);
        beam.SetPosition(1, dir * len + dir);

        //TODO
        //raycasting - hit player
    }

    //레이저 위치 및 거리 계산
    private void CalculateLaser(Vector3 dir)
    {
        /*
        if (player == Player.Player2)
            dir *= -1;

        dangerLine.SetPosition(0, dir);
        laser.SetPosition(0, dir);

        float distance = maxLen;

        RaycastHit2D hit = Physics2D.Raycast(transform.position + dir, dir, float.MaxValue, mask);
        if (hit.collider != null)
        {
            distance = Vector2.Distance(transform.position + dir, hit.point);
            Debug.Log(hit.collider.name + " " + distance);

            //TODO 포탈 충돌 시
        }

        dangerLine.SetPosition(1, dir * distance + dir);
        laser.SetPosition(1, dir * distance + dir);
        */

        //dangerLine.gameObject.SetActive(true);
        //dangerLine.SetPosition(0, dir);
        laser.SetPosition(0, dir);
        float distance = maxLen;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + dir, dir, float.MaxValue, mask);
        if (hit.collider != null)
        {
            distance = Vector2.Distance(transform.position + dir, hit.point);
            Debug.Log(hit.collider.name + " " + distance);

            //TODO 포탈 충돌 시
        }
        //dangerLine.SetPosition(1, dir * distance + dir);
        laser.SetPosition(1, dir * distance + dir);
        //dangerLine.startWidth = 0;
        //dangerLine.endWidth = 0;
        //DOTween.To(() => dangerLine.startWidth, x => dangerLine.startWidth = x, 0.5f, 1).SetEase(Ease.OutQuint);
        //DOTween.To(() => dangerLine.endWidth, x => dangerLine.endWidth = x, 0.5f, 1).SetEase(Ease.OutQuint);
        //Sequence sequence = DOTween.Sequence();
        //sequence.Append(dangerLine.DOColor(new Color2(Color.red, Color.red), new Color2(Color.white, Color.white), 1));
        //sequence.InsertCallback(1,() => { dangerLine.startColor = Color.blue; dangerLine.endColor = Color.blue; });
        //sequence.Append(dangerLine.DOColor(new Color2(Color.red,Color.red),new Color2(Color.blue,Color.blue),0.2f).SetDelay(0.1f));
    }

}
