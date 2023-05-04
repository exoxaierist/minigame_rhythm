using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//���۷�����
public static class Global
{
    //// �ý���
    // �̺�Ʈ
    public static Action OnRoundStart;

    // �Է� �ڵ鷯
    public static Action P1UpAction;
    public static Action P1DownAction;
    public static Action P1RightAction;
    public static Action P1LeftAction;
    public static Action P1SelectAction;

    public static Action P2UpAction;
    public static Action P2DownAction;
    public static Action P2RightAction;
    public static Action P2LeftAction;
    public static Action P2SelectAction;

    // UI Navigation
    public static UINavigationManager uiNavManager;

    //// ���� ����
    public static AssetCollector assets;


    //// ��ġ ���� ����
    // �׸��� ����
    public static float gridIncrement = 1;
    public static Vector2 globalOffset = Vector2.zero;


    //// ��ƿ��Ƽ
    //�ݸ��� Ȯ��
    public static bool CheckOverlap(Vector2 dest, LayerMask mask)
    {
        return Physics2D.OverlapCircle(dest, 0.3f, mask);
    }
}
