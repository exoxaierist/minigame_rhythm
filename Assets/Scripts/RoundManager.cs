using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RoundManager : MonoBehaviour
{
    public Hp hp1;
    public Hp hp2;

    int score1;
    int score2;
    private void Awake()
    {
        var obj = FindObjectsOfType<RoundManager>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        hp1 = GameObject.Find("P1").GetComponent<Hp>();
        hp2 = GameObject.Find("P2").GetComponent<Hp>();
        hp1.OnDeath += Win2p;
        hp2.OnDeath += Win1p;
    }
  
    public void Win1p()
    {
        score1 += 1;
        StartCoroutine(Delay());


    }
    public void Win2p()
    {
        score2 += 1;
        Debug.Log(score2);
        StartCoroutine(Delay());
        

    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
}

