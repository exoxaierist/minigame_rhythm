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
    public static Action OnCounterStart;
    public static Action OnCounterEnd;
    public static Action OnReset;

    // �Է� �ڵ鷯
    public static Action P1UpAction;
    public static Action P1DownAction;
    public static Action P1RightAction;
    public static Action P1LeftAction;
    public static Action P1PrimaryAction;
    public static Action P1SecondaryAction;
    public static Action P1AnyAction;
    public static Action P1UseItem;

    public static Action P2UpAction;
    public static Action P2DownAction;
    public static Action P2RightAction;
    public static Action P2LeftAction;
    public static Action P2PrimaryAction;
    public static Action P2SecondaryAction;
    public static Action P2AnyAction;
    public static Action P2UseItem;

    // ��Ʈ
    public static Func<float> GetTimingms; // ���� ����� ��Ʈ���� ms ����
    public static Func<bool> CheckBeat; // ���� Ÿ�ֿ̹� �ҷ����� ��Ʈ ������ �´���
    public static Action OnBeat; // ��Ʈ�� ���������� �Ҹ��� �̺�Ʈ

    // ���� ������
    public static int maxEnergy = 10;
    public static Action OnP1EnergyChange;
    public static Action OnP2EnergyChange;
    public static Func<int> GetP1Energy;
    public static Func<int> GetP2Energy;
    public static Func<RhythmLevel> GetP1EnergyLevel;
    public static Func<RhythmLevel> GetP2EnergyLevel;

    // UI Navigation
    public static UINavigationManager uiNavManager;
    // RhythmEnergy Manager
    public static EnergyManager energyManager;
    // Sprite Effect Manager
    public static SpriteEffectManager sprEffectManager;
    // Sound Effect Handler
    public static SoundEffectHandler sfx;

    // Pools
    public static WeaponPool weaponPool;
    public static ItemSpawner itemSpawner;


    //// ���� ����
    public static AssetCollector assets;


    //// ��ġ ���� ����
    // �׸��� ����
    public static float gridIncrement = 1;
    public static Vector2 globalOffset = Vector2.zero;

    // �ʵ� ũ�� ����
    public static Vector2 fieldExtent = new(18, 12);

    // ��ǥ�� �ʵ� �ȿ� �ִ��� t/f��ȯ
    public static bool IsInField(Vector2 pos)
    {
        if (pos.x > 0 && pos.x <= fieldExtent.x && pos.y > 0 && pos.y <= fieldExtent.y) return true;
        return false;
    }


    //// ��ƿ��Ƽ
    // ī�޶� ����
    public static CameraShaker camShaker;
    public static Action CamShakeSmall;
    public static Action CamShakeMedium;
    public static Action CamShakeLarge;

    // �ݸ��� Ȯ��
    public static bool CheckOverlap(Vector2 dest, int mask)
    {
        Collider2D col = Physics2D.OverlapPoint(dest, mask);
        return col != null;
    }

    // �ݸ��� ����ũ
    public static LayerMask playerMoveColMask = LayerMask.NameToLayer("") | LayerMask.NameToLayer("");
    public static LayerMask p1MoveColMask = 1<<LayerMask.NameToLayer("P2") | 1<<LayerMask.NameToLayer("Wall") | 1<<LayerMask.NameToLayer("Portal");
    public static LayerMask p2MoveColMask = 1<<LayerMask.NameToLayer("P1") | 1<<LayerMask.NameToLayer("Wall") | 1<<LayerMask.NameToLayer("Portal");
}
