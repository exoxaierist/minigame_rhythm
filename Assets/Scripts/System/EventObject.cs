using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �̺�Ʈ �޾Ƶ��̴� ������Ʈ�� �Ҵ�, �̺�Ʈ �߰� ����
public class EventObject : MonoBehaviour
{
    protected void EventSubscribe()
    {
        Global.OnCounterEnd += OnCounterEnd;
    }

    protected virtual void OnCounterEnd() { }
}
