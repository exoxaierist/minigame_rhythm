using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//레퍼런스용
public static class Global
{
    //// 시스템
    // 이벤트
    public static Action OnCounterStart;
    public static Action OnCounterEnd;
    public static Action OnReset;

    // 입력 핸들러
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

    // 비트
    public static Func<float> GetTimingms; // 가장 가까운 노트까지 ms 오차
    public static Func<bool> CheckBeat; // 현재 타이밍에 불렀을때 비트 오차에 맞는지
    public static Action OnBeat; // 비트에 맞을때마다 불리는 이벤트
    public static Action OnLastTiming; // 비트 마지막 타이밍에 불리는 이벤트
    public static Action OnP1MissBeat;
    public static Action OnP2MissBeat;

    // 리듬 에너지
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


    //// 에셋 참조
    public static AssetCollector assets;


    //// 매치 전역 설정
    // 그리드 설정
    public static float gridIncrement = 1;
    public static Vector2 globalOffset = Vector2.zero;

    // 필드 크기 정의
    public static Vector2 fieldExtent = new(18, 12);

    // 좌표가 필드 안에 있는지 t/f반환
    public static bool IsInField(Vector2 pos)
    {
        if (pos.x > 0 && pos.x <= fieldExtent.x && pos.y > 0 && pos.y <= fieldExtent.y) return true;
        return false;
    }


    //// 유틸리티
    // 색
    public static readonly Color whiteColor = new(0.86f, 0.85f, 0.78f);
    public static readonly Color p1Color = new(0, 0.43f, 0.89f);
    public static readonly Color p2Color = new(0.956f, 0.25f, 0.11f);

    // 카메라 진동
    public static CameraShaker camShaker;
    public static Action CamShakeSmall;
    public static Action CamShakeMedium;
    public static Action CamShakeLarge;

    // 콜리젼 확인
    public static bool CheckOverlap(Vector2 dest, int mask)
    {
        Collider2D col = Physics2D.OverlapPoint(dest, mask);
        return col != null;
    }

    // 콜리젼 마스크
    public static LayerMask playerMoveColMask = LayerMask.NameToLayer("") | LayerMask.NameToLayer("");
    public static LayerMask p1MoveColMask = 1<<LayerMask.NameToLayer("P2") | 1<<LayerMask.NameToLayer("Wall") | 1<<LayerMask.NameToLayer("Portal");
    public static LayerMask p2MoveColMask = 1<<LayerMask.NameToLayer("P1") | 1<<LayerMask.NameToLayer("Wall") | 1<<LayerMask.NameToLayer("Portal");
}
