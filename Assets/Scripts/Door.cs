using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{One,two}
public class Door : MonoBehaviour
{
    public DoorType dt;
    Animator[] doorAni;
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
        for(int k=0 ; k <doorAni.Length;k++)
        {
            BoxCollider2D childBox = doorAni[k].transform.GetChild(0).GetComponent<BoxCollider2D>();
            if (doorPoint % 2 == 0)
            {
                doorAni[k].SetBool("doorOpen", false);
                childBox.enabled = false;
            }
            else
            {
                doorAni[k].SetBool("doorOpen", true);
                childBox.enabled = true;
            }
                
        }
        
    }
    void IsDoor2()
    {
        doorPoint += 1;
        for (int k = 0; k < doorAni.Length; k++)
        {
            BoxCollider2D childBox = doorAni[k].transform.GetChild(0).GetComponent<BoxCollider2D>();
            if (doorPoint % 2 != 0)
            {
                doorAni[k].SetBool("doorOpen", false);
                childBox.enabled = false;
            }
            else
            {
                doorAni[k].SetBool("doorOpen", true);
                childBox.enabled = true;
            }

        }

    }
}
