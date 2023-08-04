using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIType { Music,Map};

public class UImanager : MonoBehaviour
{
    public GameObject rulePan;
    public GameObject SettingPan;
    public GameObject middlePan;

    public AudioSource clickBack;
    public AudioSource scrollAudio;

    public Transform[] btnGroupTrans;
    public Transform[] upTrans;
    public Transform[] downTrans;

    public SensorController[] sensCtrl;

    public Image[] sparkleImg;

    bool upGetKeyDown;
    bool downGetKeyDown;

    public int UITypenNum;
    private void Update()
    {
        UIOff();
        BtnControl();
        KeyDownControl();
    }
    
    void UIOff()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            clickBack.Play();
            if (UITypenNum == 1)
            {
                UITypenNum -= 1;
                sparkleImg[1].color = new Color(255, 255, 255, 0);
                SensorController.mapSelect = false;
                sensCtrl[1].GetComponent<BoxCollider2D>().enabled = false;
                sensCtrl[0].GetComponent<BoxCollider2D>().enabled = true;
                StartCoroutine("SparkleTime");
            }
                
            else
            {
                rulePan.SetActive(false);
                SettingPan.SetActive(false);
                middlePan.SetActive(false);
                
            }
            
        }
        if(SensorController.mapSelect)
        {
            sparkleImg[0].color = new Color(255, 255, 255, 0);
        }
    }
    void KeyDownControl()
    {

        if (Input.GetKeyDown(KeyCode.S) && sensCtrl[UITypenNum].isRoof == false && !upGetKeyDown)
        {
            scrollAudio.Play();
            StartCoroutine(KeyDelay("Up"));

        }
        else if (Input.GetKeyDown(KeyCode.W) && sensCtrl[UITypenNum].isBottom == false && !downGetKeyDown)
        {
            scrollAudio.Play();
            StartCoroutine(KeyDelay("Down"));

        }

    }
        
    void BtnControl()
    {
        if(upGetKeyDown)
        {
            btnGroupTrans[UITypenNum].position = Vector3.Lerp(btnGroupTrans[UITypenNum].position,upTrans[UITypenNum].position,20*Time.deltaTime);
            
        }
        else if (downGetKeyDown)
        {
            btnGroupTrans[UITypenNum].position = Vector3.Lerp(btnGroupTrans[UITypenNum].position, downTrans[UITypenNum].position, 20 * Time.deltaTime);
        }
    }

    public IEnumerator SparkleTime()
    {
        float sparkleCount = 0;
        while(sparkleCount<1)
        {
            sparkleCount += 0.01f;
            yield return new WaitForSeconds(0.0005f);
            sparkleImg[UITypenNum].color = new Color(255, 255, 255, sparkleCount);
        }
        if(sparkleCount >=1f)
        {
            while (sparkleCount > 0)
            {
                sparkleCount -= 0.01f;
                yield return new WaitForSeconds(0.0005f);
                sparkleImg[UITypenNum].color = new Color(255, 255, 255, sparkleCount);
            }
        }

    }

    IEnumerator KeyDelay(string type)
    {
        if(type == "Up")
        {
            upGetKeyDown = true;
            yield return new WaitForSeconds(0.3f);
            upGetKeyDown = false;
            StartCoroutine(SparkleTime());
            upTrans[UITypenNum].position += new Vector3(0, 140, 0);
            downTrans[UITypenNum].position += new Vector3(0, 140, 0);
        }
            
        else
        {
            downGetKeyDown = true;
            yield return new WaitForSeconds(0.3f);
            downGetKeyDown = false;
            StartCoroutine(SparkleTime());
            downTrans[UITypenNum].position += new Vector3(0, -140, 0);
            upTrans[UITypenNum].position += new Vector3(0, -140, 0);
        }
            
    }

}
