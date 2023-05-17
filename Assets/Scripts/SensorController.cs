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
    // Start is called before the first frame update

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
        albumImg[collision.gameObject.GetComponent<MusicInfo>().musicNum].SetActive(true);
        collision.gameObject.GetComponentInChildren<Text>().color = new Color(255, 255, 255,1);
        
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
        //albumImg[collision.gameObject.GetComponent<MusicInfo>().musicNum].SetActive(false);
    }
}
