using UnityEngine;
using Melanchall.DryWetMidi;
using Melanchall.DryWetMidi.Core;
using System.Collections.Generic;
using System;

// 스크립트에서 에셋 레퍼런싱 할 수 있게 해주는 모음집?, 뒤에 맘대로 추가
[CreateAssetMenu(fileName ="AssetCollector")]
public class AssetCollector : ScriptableObject
{
    [Header("HP바 UI")]
    public GameObject hpUI;
    public GameObject hpCounterUI;
    public Sprite hpCounterSpriteFullP1;
    public Sprite hpCounterSpriteFullP2;
    public Sprite hpCounterSpriteEmpty;

    [Header("공격 관련")]
    public GameObject bullet;
    public GameObject laser;
    public GameObject slug;
    public GameObject shield;

    [Header("이펙트")]
    public GameObject spriteEffect;
    public GameObject laserEffect;

    [Header("소리효과")] //구분위해 앞에 a붙힘
    public AudioClip aCounterHigh;
    public AudioClip aCounterLow;
    public AudioClip aShootBullet;
    public AudioClip aHitWall;
    public AudioClip aHitDamage;

    [Header("아이템")]
    public GameObject item;
    public GameObject Faint;
    public GameObject Chorus;
    public List<Sprite> itemImg;
}
