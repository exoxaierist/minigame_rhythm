using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : WeaponType
{
    LayerMask target;

    public override void SetInfo(Vector3 position, Vector3 direction, Player p, float len)
    {
        transform.position = position;
        transform.rotation = Quaternion.LookRotation(direction);
        payload.owner = p;
        isFree = false;

        payload.damage = p == Player.Player1 ? Global.GetP1Energy() : Global.GetP2Energy();

        target = payload.owner == Player.Player1 ? LayerMask.NameToLayer("P2") :LayerMask.NameToLayer("P1");
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ParticleSystem.CollisionModule pc = ps.collision;
        pc.collidesWith = 1 << target | 1 << LayerMask.NameToLayer("Wall");

        Invoke("Disable", ps.duration);
    }

    public override void Disable()
    {
        isFree = true;
        gameObject.SetActive(false);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.layer != target)
            return;

        Hp hp = other.GetComponent<Hp>();
        if (hp != null && hp.isProtected)
            hp.ShieldUnDeploy();

        OnHit(other);
        //Debug.Log("Hit");
    }

    private void OnHit(GameObject go)
    {
        IReceiveAttack receiver;
        go.TryGetComponent(out receiver);
        receiver.OnAttack(payload);
    }
}
