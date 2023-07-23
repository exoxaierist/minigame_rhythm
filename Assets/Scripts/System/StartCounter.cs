using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartCounter : MonoBehaviour
{
    private TextMeshProUGUI text;
    private int number = 3;
    private Vector3 originPos;
    public float startDelay = 2;

    private void Start() => Invoke(nameof(Initialize), startDelay);
    
    public void Initialize() => StartCoroutine(InitCoroutine());

    private IEnumerator InitCoroutine()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.color = new(1, 1, 1, 1);
        originPos = transform.position;
        
        for (int i = 3; i >= 0; i--)
        {
            Global.OnCounterStart?.Invoke();
            if (i == 0)
            {
                Global.sfx.Play(Global.assets.aCounterHigh);
                text.text = "GO!";
            }
            else
            {
                Global.sfx.Play(Global.assets.aCounterLow);
                text.text = i.ToString();
            }
            transform.DORewind();
            transform.DOShakePosition(0.3f,30, 40);
            yield return new WaitForSecondsRealtime(0.4f);
        }
        transform.DOLocalMoveY(500, 0.3f).SetEase(Ease.InQuad).OnComplete(() => transform.localPosition = new Vector3(0, -15, 0));
        text.DOColor(new(1, 1, 1, 0), 0.3f);
        Global.OnCounterEnd?.Invoke();
    }
}
