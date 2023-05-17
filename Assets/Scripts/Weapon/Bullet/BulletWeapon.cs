using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWeapon : Weapon
{
    public float recoil = 2; // ≈∫∆€¡¸, ¥‹¿ß degrees
    public float speed = 30;

    public override void P1ShootForward()
    {
        if (Global.energyManager.GetP1Energy() <= 0) return;
        Global.energyManager.DecP1Energy();

        ShootBullet(RotateVector(Vector3.left, Random.Range(-recoil, recoil)));
        ShootBullet(RotateVector(Vector3.right, Random.Range(-recoil, recoil)));
    }

    public override void P1ShootVertical()
    {
        if (Global.energyManager.GetP1Energy() <= 0) return;
        Global.energyManager.DecP1Energy();

        ShootBullet(RotateVector(Vector3.up, Random.Range(-recoil, recoil)));
        ShootBullet(RotateVector(Vector3.down, Random.Range(-recoil, recoil)));
    }

    public override void P2ShootForward()
    {
        if (Global.energyManager.GetP2Energy() <= 0) return;
        Global.energyManager.DecP2Energy();

        ShootBullet(RotateVector(Vector3.left, Random.Range(-recoil, recoil)));
        ShootBullet(RotateVector(Vector3.right, Random.Range(-recoil, recoil)));
    }

    public override void P2ShootVertical()
    {
        if (Global.energyManager.GetP2Energy() <= 0) return;
        Global.energyManager.DecP2Energy();

        ShootBullet(RotateVector(Vector3.up, Random.Range(-recoil, recoil)));
        ShootBullet(RotateVector(Vector3.down, Random.Range(-recoil, recoil)));
    }

    private Vector3 RotateVector(Vector3 direction, float offset) => Quaternion.AngleAxis(offset, Vector3.back) * direction;

    private void ShootBullet(Vector3 direction)
    {
        Global.CamShakeSmall();
        Bullet instance = Instantiate(Global.assets.bullet).GetComponent<Bullet>();
        instance.transform.position = transform.position;
        instance.direction = direction;
        instance.speed = speed;
        instance.payload = new()
        {
            owner = player,
            damage = 1
        };
        if (player == Player.Player1) instance.gameObject.layer = LayerMask.NameToLayer("P1");
        else if (player == Player.Player2) instance.gameObject.layer = LayerMask.NameToLayer("P2");
    }
}
