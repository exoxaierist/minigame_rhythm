using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Beam : MonoBehaviour
{
    public AttackInfo payload;
    public float length;
    public Vector3 dir;

    [SerializeField] float holdTime = 0.3f;
    [SerializeField] float chargeTime = 0.5f;

    private LineRenderer line;
    private LayerMask hitMask;

    private void Start()
    {
        hitMask = payload.owner == Player.Player1 ? 1 << LayerMask.NameToLayer("P2") : 1 << LayerMask.NameToLayer("P1");
        line = GetComponent<LineRenderer>();
        LaserSettings();
        StartCoroutine(HitBox());
    }

    //todo
    //레이저 이펙트 및 피격 판정

    private void LaserSettings()
    {
        line.startWidth = Global.gridIncrement;
        line.endWidth = Global.gridIncrement;
        line.SetPosition(0, dir);
        line.SetPosition(1, dir * length + dir);
        Destroy(gameObject, 2f);
    }
    IEnumerator HitBox()
    {
        float time = 0;

        DOTween.To(() => line.startWidth, x => line.startWidth = x, 0, chargeTime).SetEase(Ease.InCirc);
        DOTween.To(() => line.endWidth, x => line.endWidth = x, 0, chargeTime).SetEase(Ease.InCirc);

        yield return new WaitForSeconds(chargeTime);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, length, hitMask);
        print(hit);
            Debug.DrawLine(transform.position, Vector3.up, Color.red);

            if (hit.collider != null)
            {
                //Todo
                //상대 레이저 피격
                Debug.Log("hit!");
            }
            time += Time.deltaTime;
    }

}
