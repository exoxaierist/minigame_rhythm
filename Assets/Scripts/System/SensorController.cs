using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SensorController : MonoBehaviour
{
    public bool isRoof;
    public bool isBottom;
    public bool isOnSensor;
    static public bool mapSelect;

    public UIType uitype;

    public UImanager UIm;
    public GameObject[] albumImg;
    public GameObject[] mapImg;
    public BoxCollider2D[] sensors;

    GameObject sceneChanger;

    private void Awake()
    {
        sceneChanger = GameObject.Find("SceneManager");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if(uitype == UIType.Music)
            {
                mapSelect = true;
                UIm.UITypenNum += 1;
                UIm.StartCoroutine("SparkleTime");
                sensors[1].enabled = true;
                sensors[0].enabled = false;
            }


            if (UIm.UITypenNum > 1)
            {
                UIm.UITypenNum = 1;
                sceneChanger.GetComponent<SceneChanger>().GameStart();
            }
                
        }
        if(uitype == UIType.Map && !mapSelect)
        {
            for (int i = 0; i < mapImg.Length; i++)
            {
                mapImg[i].gameObject.SetActive(false);

            }
        }
        if (uitype == UIType.Music && mapSelect)
        {
            for (int i = 0; i < albumImg.Length; i++)
            {
                albumImg[i].gameObject.SetActive(false);

            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(UIm.UITypenNum ==0)
        {
            for (int i = 0; i < albumImg.Length; i++)
            {
                albumImg[i].gameObject.SetActive(false);

            }
        }
        else
        {
            for (int i = 0; i < mapImg.Length; i++)
            {
                mapImg[i].gameObject.SetActive(false);

            }
        }
       
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BottomBtn")
        {
            isRoof = true;
        }
        else if(collision.gameObject.tag == "TopBtn")
        {
            isBottom = true;
        }

        collision.gameObject.GetComponent<MusicInfo>().isOnSensor = true;

        if(uitype == UIType.Music)
        {
            sceneChanger.GetComponent<SceneChanger>().musicName = collision.GetComponent<MusicInfo>().musicName;
            if (UIm.UITypenNum == 0 &&!mapSelect)
                albumImg[collision.gameObject.GetComponent<MusicInfo>().mapNum].SetActive(true);
        }
        else if(uitype == UIType.Map)
        {
            sceneChanger.GetComponent<SceneChanger>().MapNum = collision.GetComponent<MusicInfo>().mapNum + 1;
            if (UIm.UITypenNum == 1 && mapSelect)
                mapImg[collision.gameObject.GetComponent<MusicInfo>().mapNum].SetActive(true);
        }
            


        if ( (int)uitype == UIm.UITypenNum)
            collision.gameObject.GetComponentInChildren<TextMeshProUGUI>().color = new Color(255, 255, 255, 1);

        if((int)uitype != UIm.UITypenNum)//uitype == UIType.Map && !mapSelect
            collision.gameObject.GetComponentInChildren<TextMeshProUGUI>().color = new Color(111, 111, 111, 0.5f);

        
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BottomBtn")
        {
            isRoof = false;
        }
        else if (collision.gameObject.tag == "TopBtn")
        {
            isBottom = false;
        }
        
        collision.gameObject.GetComponent<MusicInfo>().isOnSensor = false;
        collision.gameObject.GetComponentInChildren<TextMeshProUGUI>().color = new Color(111, 111, 111, 0.5f);
        //albumImg[collision.gameObject.GetComponent<MusicInfo>().musicNum].SetActive(false);
    }
    
}
