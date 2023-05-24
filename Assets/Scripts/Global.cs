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

    // 입력 핸들러
    public static Action P1UpAction;
    public static Action P1DownAction;
    public static Action P1RightAction;
    public static Action P1LeftAction;
    public static Action P1PrimaryAction;
    public static Action P1SecondaryAction;
    public static Action P1AnyAction;

    public static Action P2UpAction;
    public static Action P2DownAction;
    public static Action P2RightAction;
    public static Action P2LeftAction;
    public static Action P2PrimaryAction;
    public static Action P2SecondaryAction;
    public static Action P2AnyAction;

    // 비트
    public static Func<float> GetTimingms; // 가장 가까운 노트까지 ms 오차
    public static Func<bool> CheckBeat; // 현재 타이밍에 불렀을때 비트 오차에 맞는지
    public static Action OnBeat; // 비트에 맞을때마다 불리는 이벤트

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

    // Weapon Pool
    public static WeaponPool weaponPool;


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
    // 카메라 진동
    public static CameraShaker camShaker;
    public static Action CamShakeSmall;
    public static Action CamShakeMedium;
    public static Action CamShakeLarge;

    // 콜리젼 확인
    public static bool CheckOverlap(Vector2 dest, int mask)
    {
        return Physics2D.OverlapCircle(dest, 0.3f, mask);
    }

    // 콜리젼 마스크
    public static LayerMask playerMoveColMask = LayerMask.NameToLayer("") | LayerMask.NameToLayer("");
}
