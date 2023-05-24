using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    private void Awake()
    {
        Global.camShaker = this;
        Global.CamShakeSmall = ShakeSmall;
        Global.CamShakeMedium = ShakeMedium;
        Global.CamShakeLarge = ShakeLarge;
        Global.OnBeat += ShakeSmall;
    }

    public void ShakeSmall() => Shake(0.03f, 0.2f, 25);
    public void ShakeMedium() => Shake(0.08f, 0.4f, 25);
    public void ShakeLarge() => Shake(0.12f, 0.6f, 25);

    public void Shake(float amount, float duration, int vibrato)
    {
        transform.DORewind();
        transform.DOShakePosition(duration, new Vector3(amount, amount, 0),vibrato);
    }
}
