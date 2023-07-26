using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.UIElements;

public class Chorus : MonoBehaviour
{
    [SerializeField] Sprite p1Sprite;
    [SerializeField] Sprite p2Sprite;

    SpriteRenderer sr;
    Player player;
    LayerMask mask;

    float maxLen;
    int maxRepeat = 2;
    int repeatCount = 0;

    public void Init(Player player, float laserLength, Vector3 position)
    {
        transform.position = position;

        mask = 1 << LayerMask.NameToLayer("Portal") | 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("Reflect");
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = player == Player.Player1 ? p1Sprite : p2Sprite;
        this.player = player;
        maxLen = laserLength;

        Global.OnBeat += ShootLaser;
        Global.OnReset += ResetFunc;
    }

    private void ShootLaser()
    {
        if(repeatCount >= 4)
        {
            ResetFunc();
            return;
        }

        Calculate(Vector3.right, transform.position, maxLen);
        Calculate(Vector3.left, transform.position, maxLen);
        repeatCount++;
    }

    private void ResetFunc()
    {
        transform.DOScaleX(0.3f, 0f);
        transform.DOMoveY(transform.position.y + 1, 0.05f);

        repeatCount = 0;
        Global.OnBeat -= ShootLaser;
        Global.OnReset -= ResetFunc;
        Destroy(gameObject, 0.05f);
    }

    #region laser
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
                    Calculate(RotateVector(rotationDir * dir, p.RotationValue), p.transform.position, len - distance, repeat + 1);
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
        Global.weaponPool.SpawnArms(Global.assets.laser, pos, dir, player, len);
    }
    #endregion
}
