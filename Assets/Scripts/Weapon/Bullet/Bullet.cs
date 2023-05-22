using UnityEngine;

public class Bullet : WeaponType
{
    public float speed;
    public float lifetime = 6;
    public Vector3 direction;

    public LayerMask p1Mask;
    public LayerMask p2Mask;
    public LayerMask hitMask;


    private BoxCollider2D col;


    private void Update()
    {
        RaycastHit2D hit;
        hit = Physics2D.BoxCast(col.offset + transform.position * Vector2.one, col.size / 2, transform.rotation.z, direction, speed * Time.deltaTime, hitMask);
        if (hit) OnHit(hit);
        transform.position = transform.position + speed * Time.deltaTime * direction;
        //lifetime -= Time.deltaTime;
    }

    private void OnHit(RaycastHit2D hit)
    {
        if (hit.collider.gameObject.layer == gameObject.layer) return;
        
        transform.position = hit.point;
        IReceiveAttack receiver;
        hit.collider.gameObject.TryGetComponent(out receiver);
        receiver.OnAttack(payload);

        Global.sprEffectManager.SpawnEffect("hit1", hit.point, transform.rotation.eulerAngles.z - 90);

        Disable();
    }

    public void Teleport(Vector3 location)
    {
        transform.position = location;
    }

    public override void SetInfo(Vector3 position, Vector3 direction, Player p, float speed)
    {
        transform.position = position;
        this.direction = direction;
        this.speed = speed;
        payload.owner = p;
        isFree = false;

        //임시 데미지 - 플레이어에 따른 데미지 해야 됨
        payload.damage = 1;
        hitMask = payload.owner == Player.Player1 ? 1 << LayerMask.NameToLayer("P2") : 1 << LayerMask.NameToLayer("P1");
        //gameObject.layer = payload.owner == Player.Player1 ? 1 << LayerMask.NameToLayer("P1") : 1 << LayerMask.NameToLayer("P2");

        TryGetComponent(out col);
        transform.rotation = Quaternion.LookRotation(Vector3.back, direction);

        Invoke("Disable", lifetime);
    }

    protected override void Disable()
    {
        isFree = true;
        gameObject.SetActive(false);
    }
}
