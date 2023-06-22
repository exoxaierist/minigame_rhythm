using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

// �⺻������ ��ĭ�� �����̴� ��Ʈ�� ������ ������Ʈ Ŭ����
public class ControlledObject : GridObject
{
    [Header("�÷��̾�")]
    public Player player = Player.Player1;
    [Header("�浹 ���̾�")]
    public int collisionLayer;

    protected virtual void Awake()
    {
        Global.OnCounterEnd += SubscribeToInput;
    }

    protected virtual void MoveUp() {}
    protected virtual void MoveDown() {}
    protected virtual void MoveRight() {}
    protected virtual void MoveLeft() {}

    // ��ǲ �븮�ڿ��� ����
    protected void UnsubscribeToInput()
    {
        Global.P2UpAction -= MoveUp;
        Global.P2DownAction -= MoveDown;
        Global.P2RightAction -= MoveRight;
        Global.P2LeftAction -= MoveLeft;

        Global.P1UpAction -= MoveUp;
        Global.P1DownAction -= MoveDown;
        Global.P1RightAction -= MoveRight;
        Global.P1LeftAction -= MoveLeft;
    }

    // ��ǲ �븮�ڿ� �߰�
    protected void SubscribeToInput()
    {
        UnsubscribeToInput();
        if (player == Player.Player1)
        {
            Global.P1UpAction += MoveUp;
            Global.P1DownAction += MoveDown;
            Global.P1RightAction += MoveRight;
            Global.P1LeftAction += MoveLeft;
        }
        else if (player == Player.Player2)
        {
            Global.P2UpAction += MoveUp;
            Global.P2DownAction += MoveDown;
            Global.P2RightAction += MoveRight;
            Global.P2LeftAction += MoveLeft;
        }
    }

}
