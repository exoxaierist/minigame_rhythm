using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotalManager : MonoBehaviour
{
    public Transform[] potalTrans;
    public Transform[] otherPotalTrans;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < potalTrans.Length;i++)
        {
            potalTrans[i].GetComponent<Potal>().bulletPos = otherPotalTrans[i].transform;
        }
    }
}
