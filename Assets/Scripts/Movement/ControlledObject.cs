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
    public LayerMask collisionLayer;

    protected virtual void Awake()
    {
        SubscribeToInput();
    }

    protected virtual void MoveUp()
    {
        if(!Global.CheckOverlap(transform.position*Vector2.one + new Vector2(0,1),collisionLayer)) MoveRelative(new Vector2(0, 1));
    }
    protected virtual void MoveDown()
    {
        if (!Global.CheckOverlap(transform.position * Vector2.one + new Vector2(0, -1), collisionLayer)) MoveRelative(new Vector2(0, -1));
    }
    protected virtual void MoveRight()
    {
        if (!Global.CheckOverlap(transform.position * Vector2.one + new Vector2(1, 0), collisionLayer)) MoveRelative(new Vector2(1, 0));
    }
    protected virtual void MoveLeft()
    {
        if (!Global.CheckOverlap(transform.position * Vector2.one + new Vector2(-1,0), collisionLayer)) MoveRelative(new Vector2(-1, 0));
    }

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
        //Global.P1SpecialAction -= MoveSpecial;
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
            //Global.P1SpecialAction += MoveSpecial;
        }
        else if (player == Player.Player2)
        {
            Global.P2UpAction += MoveUp;
            Global.P2DownAction += MoveDown;
            Global.P2RightAction += MoveRight;
            Global.P2LeftAction += MoveLeft;
            //Global.P2SpecialAction += MoveSpecial;
        }
    }

}
