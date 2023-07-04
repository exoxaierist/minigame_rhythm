using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{One,two}
public class Door : MonoBehaviour
{
    public DoorType dt;
    Animator[] doorAni;
    int door4Point;
    int doorPoint;
    private void Awake()
    {
        doorAni = GetComponentsInChildren<Animator>();
        if(dt == DoorType.One)
            Global.OnBeat += IsDoor1;
        else
            Global.OnBeat += IsDoor2;

    }
    void IsDoor1()
    {
        
        doorPoint += 1;
        if(doorPoint %4 == 0)
        {
            door4Point += 1;
        }
        for(int k=0 ; k <doorAni.Length;k++)
        {
            if (door4Point % 2 == 0)
            {
                doorAni[k].SetBool("doorOpen", false);
                doorAni[k].gameObject.GetComponent<BoxCollider2D>().enabled = false;
                
            }
            else
            {
                doorAni[k].SetBool("doorOpen", true);
                doorAni[k].gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }

        }
        
    }
    void IsDoor2()
    {
        doorPoint += 1;
        if (doorPoint % 4 == 0)
        {
            door4Point += 1;
        }
        for (int k = 0; k < doorAni.Length; k++)
        {
            if (door4Point % 2 != 0)
            {
                doorAni[k].SetBool("doorOpen", false);
                doorAni[k].gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                doorAni[k].SetBool("doorOpen", true);
                doorAni[k].gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }

        }

    }
}
