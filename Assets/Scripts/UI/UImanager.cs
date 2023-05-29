using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public GameObject rulePan;
    public GameObject SettingPan;
    public GameObject middlePan;

    public Transform btnGroupTrans;
    public Transform upTrans;
    public Transform downTrans;

    public SensorController sensCtrl;

    public Image sparkleImg;

    bool upGetKeyDown;
    bool downGetKeyDown;
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
            rulePan.SetActive(false);
            SettingPan.SetActive(false);
            middlePan.SetActive(false);
        }
    }
    void KeyDownControl()
    {

        if (Input.GetKeyDown(KeyCode.W) && sensCtrl.isRoof == false && !upGetKeyDown)
        {
            StartCoroutine(KeyDelay("Up"));

        }
        else if (Input.GetKeyDown(KeyCode.S) && sensCtrl.isBottom == false && !downGetKeyDown)
        {
            StartCoroutine(KeyDelay("Down"));

        }

    }
        
    void BtnControl()
    {
        if(upGetKeyDown)
        {
            btnGroupTrans.position = Vector3.Lerp(btnGroupTrans.position,upTrans.position,20*Time.deltaTime);
            
        }
        else if (downGetKeyDown)
        {
            btnGroupTrans.position = Vector3.Lerp(btnGroupTrans.position, downTrans.position, 20 * Time.deltaTime);
        }
    }

    IEnumerator SparkleTime()
    {
        float sparkleCount = 0;
        while(sparkleCount<1)
        {
            sparkleCount += 0.01f;
            yield return new WaitForSeconds(0.0005f);
            sparkleImg.color = new Color(255, 255, 255, sparkleCount);
        }
        if(sparkleCount >=1f)
        {
            while(sparkleCount>0)
            {
                sparkleCount -= 0.01f;
                yield return new WaitForSeconds(0.0005f);
                sparkleImg.color = new Color(255, 255, 255, sparkleCount);
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
            upTrans.position += new Vector3(0, 140, 0);
            downTrans.position += new Vector3(0, 140, 0);
        }
            
        else
        {
            downGetKeyDown = true;
            yield return new WaitForSeconds(0.3f);
            downGetKeyDown = false;
            StartCoroutine(SparkleTime());
            downTrans.position += new Vector3(0, -140, 0);
            upTrans.position += new Vector3(0, -140, 0);
        }
            
    }

}
