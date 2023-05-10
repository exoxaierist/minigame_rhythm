using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyCounterUI : MonoBehaviour
{
    public Color activatedColor = Color.white;
    public Color deactivatedColor = new(1,1,1,0.5f);
    public Color level1Color = Color.green;
    public Color level2Color = Color.cyan;
    public Color level3Color = Color.blue;

    private Image image;

    private int currentLevel;

    private void Awake()
    {
        if (!TryGetComponent(out image)) enabled = false;
        Deactivate();
    }

    public void Activate(int level, float delay)
    {
        SetLevel(level, delay);
        transform.DORewind();
        transform.DOPunchScale(new(0.15f, 0.15f, 0), 0.2f);
    }

    public void Deactivate()
    {
        image.DOKill();
        image.DOColor(deactivatedColor, 0.1f);
    }

    public void SetLevel(int level, float delay)
    {
        switch (level)
        {
            case 0:
                image.DOColor(activatedColor, 0.1f).SetDelay(delay);
                transform.DORewind();
                transform.DOPunchScale(new(0.1f, 0.1f, 0), 0.2f);
                break;
            case 1:
                image.DOColor(level1Color, 0.1f).SetDelay(delay);
                transform.DORewind();
                transform.DOPunchScale(new(0.1f, 0.1f, 0), 0.2f);
                break;
            case 2:
                image.DOColor(level2Color, 0.1f).SetDelay(delay);
                transform.DORewind();
                transform.DOPunchScale(new(0.1f, 0.1f, 0), 0.2f);
                break;
            case 3:
                image.DOColor(level3Color, 0.1f).SetDelay(delay);
                transform.DORewind();
                transform.DOPunchScale(new(0.1f, 0.1f, 0), 0.2f);
                break;
        }
    }
}
