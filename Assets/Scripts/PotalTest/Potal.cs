using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potal : MonoBehaviour
{
    public Transform bulletPos;
    public Player playerType;
    public Attack attackInfo;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        




    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ok");
        if (collision.gameObject.tag == "Bullet" && playerType == Player.Player1)
        {
            Debug.Log("ok");
            collision.transform.position = bulletPos.position + new Vector3(0, 1, 0);
        }
        if (collision.gameObject.tag == "Bullet" && playerType == Player.Player2)
        {
            Debug.Log("ok");
            collision.transform.position = bulletPos.position + new Vector3(0, -1, 0);
        }
    }
    private void OnTriggerEnter2D(Collision other)
    {
        
        
    }
  

}
