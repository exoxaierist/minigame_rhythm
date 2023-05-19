using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_OnBeatInvoke : MonoBehaviour
{
    public void Hop()
    {
        Global.OnBeat?.Invoke();
    }
}
