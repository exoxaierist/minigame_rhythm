using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class UINavigationManager : MonoBehaviour
{
    private List<UINavigatable> navList = new();

    public List<UINavigatable> GetAllNavigatable() { return navList; }
    public UINavigatable currentSelected;

    public GameObject p1Sel;

    private void Awake()
    {
        Global.uiNavManager = this;
        navList = FindObjectsOfType<UINavigatable>().ToList();
        foreach (UINavigatable nav in navList) nav.SearchNavigatable();
        currentSelected = navList[0];
    }

    private void Start()
    {
        p1Sel.transform.position = currentSelected.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && currentSelected.right!=null)
        {
            currentSelected = currentSelected.right;
            p1Sel.transform.position = currentSelected.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentSelected.left != null)
        {
            currentSelected = currentSelected.left;
            p1Sel.transform.position = currentSelected.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && currentSelected.up != null)
        {
            currentSelected = currentSelected.up;
            p1Sel.transform.position = currentSelected.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && currentSelected.down != null)
        {
            currentSelected = currentSelected.down;
            p1Sel.transform.position = currentSelected.transform.position;
        }
    }
}
