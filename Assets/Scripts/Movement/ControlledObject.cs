using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

// 기본적으로 한칸씩 움직이는 컨트롤 가능한 오브젝트 클래스
public class ControlledObject : GridObject
{
    [Header("플레이어")]
    public Player player = Player.Player1;
    [Header("충돌 레이어")]
    public int collisionLayer;

    protected virtual void Awake()
    {
        Global.OnCounterEnd += SubscribeToInput;
    }

    protected virtual void MoveUp() {}
    protected virtual void MoveDown() {}
    protected virtual void MoveRight() {}
    protected virtual void MoveLeft() {}

    // 인풋 대리자에서 제거
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

    // 인풋 대리자에 추가
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
