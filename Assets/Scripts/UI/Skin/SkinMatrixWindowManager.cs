using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinMatrixWindowManager : MonoBehaviour
{
    public int index;
    public string charaName;
    public GameObject parchaseWindow;
    public GameObject cancelWindow;

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
        int coin = PlayerPrefs.GetInt("coin", 0);
        switch (SkinViewManager.nowSkin)
        {
            case (int)SkinViewManager.skinType.soldier:
                if (SkinManager.BuyFlag("soldier", (SkinViewManager.page - 1) * 6 + index))
                {
                    SkinManager.SkinChange("soldier", (SkinViewManager.page - 1) * 6 + index);
                }
                else
                {
                    var(_,_,price,_) = SkinManager.SoldierSkin[(SkinViewManager.page - 1) * 6 + index];
                    if (coin >= price)
                    {
                        parchaseWindow.SetActive(true);
                    }
                    else
                    {
                        cancelWindow.SetActive(true);
                    }
                }
                break;
            case (int)SkinViewManager.skinType.tank:
                if (SkinManager.BuyFlag("tank", (SkinViewManager.page - 1) * 6 + index))
                {
                    SkinManager.SkinChange("tank", (SkinViewManager.page - 1) * 6 + index);
                }
                else
                {
                    var (_, _, price, _) = SkinManager.TankSkin[(SkinViewManager.page - 1) * 6 + index];
                    if (coin >= price)
                    {
                        parchaseWindow.SetActive(true);
                    }
                    else
                    {
                        cancelWindow.SetActive(true);
                    }
                }
                break;
            case (int)SkinViewManager.skinType.fighter:
                if (SkinManager.BuyFlag("fighter", (SkinViewManager.page - 1) * 6 + index))
                {
                    SkinManager.SkinChange("fighter", (SkinViewManager.page - 1) * 6 + index);
                }
                else
                {
                    var (_, _, price, _) = SkinManager.FighterSkin[(SkinViewManager.page - 1) * 6 + index];
                    if (coin >= price)
                    {
                        parchaseWindow.SetActive(true);
                    }
                    else
                    {
                        cancelWindow.SetActive(true);
                    }
                }
                break;
        }
        
    }

    public void Purchase()
    {
        switch (SkinViewManager.nowSkin)
        {
            case (int)SkinViewManager.skinType.soldier:
                if (SkinManager.Available("soldier", (SkinViewManager.page - 1) * 6 + index))
                {
                    SkinManager.Purchase("soldier", (SkinViewManager.page - 1) * 6 + index);
                    SkinManager.SkinChange("soldier", (SkinViewManager.page - 1) * 6 + index);
                }
                else
                {
                    SkinManager.SkinChange("soldier", (SkinViewManager.page - 1) * 6 + index);
                }
                break;
            case (int)SkinViewManager.skinType.tank:
                if (SkinManager.Available("tank", (SkinViewManager.page - 1) * 6 + index))
                {
                    SkinManager.Purchase("tank", (SkinViewManager.page - 1) * 6 + index);
                    SkinManager.SkinChange("tank", (SkinViewManager.page - 1) * 6 + index);
                }
                else
                {
                    SkinManager.SkinChange("tank", (SkinViewManager.page - 1) * 6 + index);
                }
                break;
            case (int)SkinViewManager.skinType.fighter:
                if (SkinManager.Available("fighter", (SkinViewManager.page - 1) * 6 + index))
                {
                    SkinManager.Purchase("fighter", (SkinViewManager.page - 1) * 6 + index);
                    SkinManager.SkinChange("fighter", (SkinViewManager.page - 1) * 6 + index);
                }
                else
                {
                    SkinManager.SkinChange("fighter", (SkinViewManager.page - 1) * 6 + index);
                }
                break;
        }
        
    }
}
