using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject bulletPrf;
    public Player playerType;
    public int rayDistance = 5;
    public RaycastHit2D rayHit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rayHit = Physics2D.Raycast(transform.position + new Vector3(0, 1f, 0), transform.up,rayDistance);
        if (rayHit)
        {
            Debug.Log("Hit Green " + rayHit.collider.gameObject.name);
            Debug.DrawRay(transform.position+new Vector3(0, 1f, 0), transform.up * rayHit.distance, Color.red);
            Debug.DrawRay(rayHit.collider.gameObject.GetComponent<Potal>().bulletPos.position + new Vector3(0, 1f, 0), transform.up * (rayDistance - rayHit.distance), Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position+ new Vector3(0, 1f, 0), transform.up * rayDistance, Color.green);
        }
        //Debug.DrawRay(gameObject.transform.position, gameObject.transform.up * rayDistance, Color.green);
        //if (playerType == Player.Player1 && Input.GetKeyDown(KeyCode.F))
        //{
        //    Debug.Log("1");
        //    bulletPrf.GetComponent<Bullet>().speed = 0.01f;
        //    Instantiate(bulletPrf, transform.position, transform.rotation);
        //}
            
        //else if (playerType == Player.Player2 && Input.GetKeyDown(KeyCode.M))
        //{
        //    bulletPrf.GetComponent<Bullet>().speed = -0.01f;
        //    Instantiate(bulletPrf, transform.position, transform.rotation);
        //    Debug.Log("2");
        //}
            

    }
}
