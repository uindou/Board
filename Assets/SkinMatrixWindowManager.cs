using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinMatrixWindowManager : MonoBehaviour
{
    public int index;
    public string charaName;
    public GameObject parchaseWindow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ViewPurchaseWindow()
    {
        if (SkinManager.BuyFlug("soldier", (SkinViewManager.page - 1) * 6 + index)){
            parchaseWindow.gameObject.SetActive(true);
        }
        else
        {

        }
    }

    public void Purchase()
    {
        SkinManager.Purchase("soldier", (SkinViewManager.page - 1) * 6 + index);
    }
}
