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

    public AudioSource enterAudio;

    GameObject sceneChanger;

    private void Awake()
    {
        sceneChanger = GameObject.Find("SceneManager");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (uitype == UIType.Music)
            {
                mapSelect = true;
                UIm.UITypenNum += 1;
                UIm.StartCoroutine("SparkleTime");
                sensors[1].enabled = true;
                sensors[0].enabled = false;
                enterAudio.Play();
            }


            if (UIm.UITypenNum > 1)
            {
                UIm.UITypenNum = 1;
                enterAudio.Play();
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
        if (uitype == UIType.Music)
        {
            GameObject.Find("Canvas").transform.Find("GameSelectPanel").Find("Background").GetComponent<Image>().sprite 
                = albumImg[collision.gameObject.GetComponent<MusicInfo>().mapNum].GetComponent<Image>().sprite;
            string mName = collision.GetComponent<MusicInfo>().musicName;
            sceneChanger.GetComponent<SceneChanger>().musicName = mName;
            if (UIm.UITypenNum == 0 && !mapSelect)
                albumImg[collision.gameObject.GetComponent<MusicInfo>().mapNum].gameObject.SetActive(true);
            foreach (AudioClip clip in MusicList.instance.BGM)
            {
                if (clip.name == mName)
                {
                    AudioSource audioSource = GameObject.Find("TestAudio").transform.GetChild(0).GetComponent<AudioSource>();
                    audioSource.clip = clip;
                    audioSource.Play();
                    break;
                }

            }
        }
        else if (uitype == UIType.Map)
        {
            sceneChanger.GetComponent<SceneChanger>().MapNum = collision.GetComponent<MusicInfo>().mapNum + 1;
            if (UIm.UITypenNum == 1 && mapSelect)
                mapImg[collision.gameObject.GetComponent<MusicInfo>().mapNum].gameObject.SetActive(true);
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
