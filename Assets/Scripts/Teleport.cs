using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform otherPos;
    static public bool canport = true;
    static public int playerCount;

    public bool teleportDelay;

    int activeCount;
    int active4count;

    float scalingDuration = 0.5f;

    SpriteRenderer spr;
    BoxCollider2D boxcol;

    private void Awake()
    {
        Global.OnBeat += TeleportActive;
        spr = GetComponent<SpriteRenderer>();
        boxcol = GetComponent<BoxCollider2D>();
    }
    void TeleportActive()
    {
        activeCount += 1;
        if(activeCount %4 == 0)
        {
            active4count += 1;
        }
        if(active4count%2 == 0)
        {
            canport = true;
            
        }
        else if (active4count%2 !=0)
        {
            canport = false;
        }
        teleportDelay = false;
    }
    private void Update()
    {
        if(canport)
        {
            boxcol.enabled = true;
            spr.color = Color.white;

        }

        else if(!canport)
        {
            boxcol.enabled = false;
            spr.color = Color.gray;
        }
        //if (playerCount == 2)
        //    canport = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "P1")
            playerCount += 1;
        if (collision.gameObject.tag == "P2")
            playerCount += 1;

        
        if (canport && !teleportDelay)
        {
            StartCoroutine(TeleportIn(collision.transform, collision.transform.localScale));
            collision.transform.position = otherPos.position;
            StartCoroutine(TeleportOut(collision.transform, collision.transform.localScale));
            otherPos.GetComponent<Teleport>().teleportDelay = true;
        }


        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "P1")
            playerCount -= 1;
        if (collision.gameObject.tag == "P2")
            playerCount -= 1;
        collision.transform.localScale = collision.gameObject.GetComponent<PlayerBase>().originscale;
    }
    IEnumerator TeleportIn(Transform playerTrans, Vector3 originalScale)
    {

        
        float elapsedTime = 0f;
        while (elapsedTime < scalingDuration)
        {
            float t = elapsedTime / scalingDuration;
            playerTrans.localScale = Vector3.Lerp(originalScale, Vector3.zero, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // 크기가 0이 된 상태에서 1초 안에 원래 크기로 돌아오기
        
    }
    IEnumerator TeleportOut(Transform playerTrans, Vector3 originalScale)
    {
        float elapsedTime = 0f;
        while (elapsedTime < scalingDuration)
        {
            float t = elapsedTime / scalingDuration;
            playerTrans.localScale = Vector3.Lerp(Vector3.zero, originalScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 보장되지 않은 경우를 대비해 크기를 명시적으로 원래 크기로 설정
        transform.localScale = originalScale;
    }


}
