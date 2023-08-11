using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class SoundOptions : MonoBehaviour
{
    // 오디오 믹서
    public AudioMixer audioMixer;

    // 슬라이더
    public Slider BgmSlider;
    public Slider SfxSlider;

    public void Awake()
    {
        float currentValue;
        audioMixer.GetFloat("BGM", out currentValue);
        BgmSlider.value = Mathf.Pow(10, (currentValue / 20));
        audioMixer.GetFloat("SFX", out currentValue);
        SfxSlider.value = Mathf.Pow(10, (currentValue / 20));
    }

    // 볼륨 조절
    public void SetBgmVolme()
    {
         // 로그 연산 값 전달
        audioMixer.SetFloat("BGM", Mathf.Log10(BgmSlider.value) * 20);
    }

    public void SetSFXVolme()
    {
         // 로그 연산 값 전달
        audioMixer.SetFloat("SFX", Mathf.Log10(SfxSlider.value) * 20);
    }
}