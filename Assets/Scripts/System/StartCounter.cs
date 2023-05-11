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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Initialize();   
    }

    public void Initialize() => StartCoroutine(InitCoroutine());

    private IEnumerator InitCoroutine()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.color = new(1, 1, 1, 1);
        originPos = transform.position;
        
        for (int i = 3; i >= 0; i--)
        {
            if (i == 0) { text.text = "GO!"; }
            else text.text = i.ToString();
            transform.DORewind();
            transform.DOShakePosition(0.3f,30, 40);
            yield return new WaitForSecondsRealtime(0.4f);
        }
        
        transform.DOLocalMoveY(500, 0.3f).SetEase(Ease.InQuad);
        text.DOColor(new(1, 1, 1, 0), 0.3f);
    }
}
