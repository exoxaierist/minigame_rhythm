using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWeapon : MonoBehaviour
{
    public float recoil = 10; // ÅºÆÛÁü, ´ÜÀ§ degrees
    public Player player = Player.Player1;

    private void Awake()
    {
        Global.P1SelectAction += ShootTest;
    }

    public void ShootTest()
    {
        ShootBullet(RotateVector(Vector3.right,Random.Range(-recoil,recoil)));
    }

    private Vector3 RotateVector(Vector3 direction, float offset) => Quaternion.AngleAxis(offset, Vector3.back) * direction;

    private void ShootBullet(Vector3 direction)
    {
        Global.CamShakeSmall();
        Bullet instance = Instantiate(Global.assets.bullet).GetComponent<Bullet>();
        instance.transform.position = transform.position;
        instance.direction = direction;
        instance.speed = 15;
        instance.payload = new AttackInfo();
        instance.payload.owner = player;
        instance.payload.damage = 1;
        if (player == Player.Player1) instance.gameObject.layer = LayerMask.NameToLayer("P1");
        else if (player == Player.Player2) instance.gameObject.layer = LayerMask.NameToLayer("P2");
    }
}
