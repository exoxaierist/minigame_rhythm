using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensorController : MonoBehaviour
{
    public bool isRoof;
    public bool isBottom;
    public bool isOnSensor;

    public GameObject[] albumImg;

    GameObject sceneChanger;

    private void Awake()
    {
        sceneChanger = GameObject.Find("SceneManager");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            sceneChanger.GetComponent<SceneChanger>().GameStart();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        for(int i = 0;i<albumImg.Length;i++)
        {
            albumImg[i].gameObject.SetActive(false);

        }
       
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Music7")
        {
            isRoof = true;
        }
        else if(collision.gameObject.name == "Music1")
        {
            isBottom = true;
        }
        collision.gameObject.GetComponent<MusicInfo>().isOnSensor = true;
        albumImg[collision.gameObject.GetComponent<MusicInfo>().musicNum].SetActive(true);
        collision.gameObject.GetComponentInChildren<Text>().color = new Color(255, 255, 255, 1);
        sceneChanger.GetComponent<SceneChanger>().musicName = collision.GetComponent<MusicInfo>().musicName;
        

    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Music7")
        {
            isRoof = false;
        }
        else if (collision.gameObject.name == "Music1")
        {
            isBottom = false;
        }
        collision.gameObject.GetComponent<MusicInfo>().isOnSensor = false;
        collision.gameObject.GetComponentInChildren<Text>().color = new Color(111, 111, 111, 0.5f);
        //albumImg[collision.gameObject.GetComponent<MusicInfo>().musicNum].SetActive(false);
    }
    
}
