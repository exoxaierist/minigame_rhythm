using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorRotation : MonoBehaviour
{
    Animator ani;
    int turnCount;
    [SerializeField]
    int turn4Count;
    private void Awake()
    {
        ani = GetComponent<Animator>();
        Global.OnBeat += RotateMirror;
    }

    void RotateMirror()
    {
        turnCount += 1;
        if(turnCount%4 ==0)
        {
            turn4Count += 1;
        }
        ani.SetFloat("isTurn", turn4Count);
        if(turn4Count%4==0)
        {
            turn4Count = 0;
        }
    }
}
