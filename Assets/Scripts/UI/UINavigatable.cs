using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINavigatable : MonoBehaviour
{
    public Player player = Player.Player1;

    public UINavigatable right;
    public UINavigatable left;
    public UINavigatable up;
    public UINavigatable down;

    public void SearchNavigatable()
    {
        right = SearchRight();
        left = SearchLeft();
        up = SearchUp();
        down = SearchDown();
    }

    private UINavigatable SearchRight()
    {
        List<UINavigatable> all = Global.uiNavManager.GetAllNavigatable();
        float largestDot = 100000;
        UINavigatable result = null;
        
        foreach (UINavigatable nav in all)
        {
            if (player == nav.player || nav.player == Player.All)
            {
                if (nav.transform.position.x > transform.position.x)
                {
                    Vector3 dir = nav.transform.position - transform.position;
                    float dot = (1 - Vector3.Dot(Vector3.right, dir.normalized)) * dir.sqrMagnitude;
                    if (dot < largestDot)
                    {
                        result = nav;
                        largestDot = dot;
                    }
                }
            }
        }
        return result;
    }
    private UINavigatable SearchLeft()
    {
        List<UINavigatable> all = Global.uiNavManager.GetAllNavigatable();
        float largestDot = 100000;
        UINavigatable result = null;

        foreach (UINavigatable nav in all)
        {
            if (player == nav.player || nav.player == Player.All)
            {
                if (nav.transform.position.x < transform.position.x)
                {
                    Vector3 dir = nav.transform.position - transform.position;
                    float dot = (1 - Vector3.Dot(Vector3.left, dir.normalized)) * dir.sqrMagnitude;
                    if (dot < largestDot)
                    {
                        result = nav;
                        largestDot = dot;
                    }
                }
            }
        }
        return result;
    }
    private UINavigatable SearchUp()
    {
        List<UINavigatable> all = Global.uiNavManager.GetAllNavigatable();
        float largestDot = 100000;
        UINavigatable result = null;

        foreach (UINavigatable nav in all)
        {
            if (player == nav.player || nav.player == Player.All)
            {
                if (nav.transform.position.y > transform.position.y)
                {
                    Vector3 dir = nav.transform.position - transform.position;
                    float dot = (1 - Vector3.Dot(Vector3.up, dir.normalized)) * dir.sqrMagnitude;
                    if (dot < largestDot)
                    {
                        result = nav;
                        largestDot = dot;
                    }
                }
            }
        }
        return result;
    }
    private UINavigatable SearchDown()
    {
        List<UINavigatable> all = Global.uiNavManager.GetAllNavigatable();
        float largestDot = 100000;
        UINavigatable result = null;

        foreach (UINavigatable nav in all)
        {
            if (player == nav.player || nav.player == Player.All)
            {
                
                if (nav.transform.position.y < transform.position.y)
                {
                    Vector3 dir = nav.transform.position - transform.position;
                    float dot = (1 - Vector3.Dot(Vector3.down, dir.normalized)) * dir.sqrMagnitude;
                    if (dot < largestDot)
                    {
                        result = nav;
                        largestDot = dot;
                    }
                }
            }
        }
        return result;
    }
}
