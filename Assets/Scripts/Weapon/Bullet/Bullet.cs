using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime = 6;
    public Vector3 direction;
    public AttackInfo payload;

    private void Start()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.back,direction);
    }

    private void Update()
    {
        transform.position = transform.position + direction * speed * Time.deltaTime;
        lifetime -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == gameObject.layer) return;
        IReceiveAttack receiver;
        collision.gameObject.TryGetComponent(out receiver);
        receiver.OnAttack(payload);
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
