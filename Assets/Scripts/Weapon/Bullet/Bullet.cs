using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime = 6;
    public Vector3 direction;
    public AttackInfo payload;

    public LayerMask p1Mask;
    public LayerMask p2Mask;

    private BoxCollider2D col;

    private void Start()
    {
        TryGetComponent(out col);
        transform.rotation = Quaternion.LookRotation(Vector3.back,direction);
    }

    private void Update()
    {
        RaycastHit2D hit;
        hit = Physics2D.BoxCast(col.offset + transform.position * Vector2.one, col.size / 2, transform.rotation.z, direction, speed * Time.deltaTime,payload.owner==Player.Player1?p1Mask:p2Mask);
        if (hit) OnHit(hit);
        transform.position = transform.position + speed * Time.deltaTime * direction;
        lifetime -= Time.deltaTime;
    }

    private void OnHit(RaycastHit2D hit)
    {
        if (hit.collider.gameObject.layer == gameObject.layer) return;
        
        transform.position = hit.point;
        IReceiveAttack receiver;
        hit.collider.gameObject.TryGetComponent(out receiver);
        receiver.OnAttack(payload);

        Global.sprEffectManager.SpawnEffect("hit1", hit.point, transform.rotation.eulerAngles.z - 90);

        Kill();
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    public void Teleport(Vector3 location)
    {
        transform.position = location;
    }
}
