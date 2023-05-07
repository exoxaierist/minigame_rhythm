using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public abstract class Weapon : MonoBehaviour
{
    private int rhythmEnergy = 0;
    public int damage = 0;
    protected bool isShooting = false;
    protected Vector2 pos;

    public enum RhythmLevel
    {
        Zero = 0,
        One = 1,
        Two = 2,
        Three = 3,
    }

    [Header("에너지 단계")]
    public RhythmLevel rhythmLevel = RhythmLevel.Zero;
    [SerializeField]
    protected Player player;

    public abstract void Shoot(KeyCode key);

    public void FailToShoot()
    {
        rhythmEnergy = 0;
        rhythmLevel = RhythmLevel.Zero;

        //TODO 공격실패 모션
    }

    public void IncRhythmEnergy()
    {
        rhythmEnergy++;
    }

    protected void ResetEnergy()
    {
        rhythmEnergy = 0;
        rhythmLevel = RhythmLevel.Zero;

        damage = 0;
    }

    protected void CheckEnergy()
    {
        if (rhythmEnergy < 1) rhythmLevel = RhythmLevel.Zero;
        else if (rhythmEnergy < 4) rhythmLevel = RhythmLevel.One;
        else if(rhythmEnergy < 8) rhythmLevel = RhythmLevel.Two;
        else rhythmLevel = RhythmLevel.Three;
    }

}
