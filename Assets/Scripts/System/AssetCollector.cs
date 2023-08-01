using UnityEngine;
using Melanchall.DryWetMidi;
using Melanchall.DryWetMidi.Core;
using System.Collections.Generic;
using System;

// ��ũ��Ʈ���� ���� ���۷��� �� �� �ְ� ���ִ� ������?, �ڿ� ����� �߰�
[CreateAssetMenu(fileName ="AssetCollector")]
public class AssetCollector : ScriptableObject
{
    [Header("HP�� UI")]
    public GameObject hpUI;
    public GameObject hpCounterUI;
    public Sprite hpCounterSpriteFullP1;
    public Sprite hpCounterSpriteFullP2;
    public Sprite hpCounterSpriteEmpty;

    [Header("���� ����")]
    public GameObject bullet;
    public GameObject laser;
    public GameObject slug;
    public GameObject shield;

    [Header("����Ʈ")]
    public GameObject spriteEffect;
    public GameObject laserEffect;

    [Header("�Ҹ�ȿ��")] //�������� �տ� a����
    public AudioClip aCounterHigh;
    public AudioClip aCounterLow;
    public AudioClip aShootBullet;
    public AudioClip aHitWall;
    public AudioClip aHitDamage;
    public AudioClip aTeleport;
    public AudioClip aLaserFire;
    public AudioClip aLaserHit;
    public AudioClip aNotEnoughEnergy;
    public AudioClip aGainEnergy;
    public AudioClip aMissBeat;
    public AudioClip aGetItem;
    public AudioClip aUseItem;
    public AudioClip aShieldDefense;

    [Header("������")]
    public GameObject item;
    public GameObject Faint;
    public GameObject Chorus;
    public List<Sprite> itemImg;
}
