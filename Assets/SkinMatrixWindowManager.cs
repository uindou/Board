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
        if (SkinManager.BuyFlag("soldier", (SkinViewManager.page - 1) * 6 + index)){
            SkinManager.SkinChange("soldier", (SkinViewManager.page - 1) * 6 + index);
        }
        else
        {
            parchaseWindow.gameObject.SetActive(true);
        }
    }

    public void Purchase()
    {
        Debug.Log(index);
        if(SkinManager.Available("soldier", (SkinViewManager.page - 1) * 6 + index))
        {
            SkinManager.Purchase("soldier", (SkinViewManager.page - 1) * 6 + index);
        }
        else
        {
            SkinManager.SkinChange("soldier", (SkinViewManager.page - 1) * 6 + index);
        }
    }
}
