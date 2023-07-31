using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform otherPos;
    public bool canport = true;
    int activeCount;
    int active4count;

    private void Awake()
    {
        Global.OnBeat += TeleportActive;
    }
    void TeleportActive()
    {
        activeCount += 1;
        if(activeCount %4 == 0)
        {
            active4count += 1;
        }
        if(active4count%2 == 0)
        {
            canport = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "P1"|| collision.gameObject.tag == "P2")
        {
            if(canport)
            {
                collision.transform.position = otherPos.position;
                otherPos.GetComponent<Teleport>().canport = false;
            }
                
        }
    }
}
