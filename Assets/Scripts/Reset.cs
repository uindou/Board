using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public GameObject closeWindow;
    // Start is called before the first frame update
    public void OnClick()
    {
        Debug.Log(1);
        PlayerPrefs.SetInt("coin", 10000);
        PlayerPrefs.SetInt("endFirst", 0);
        PlayerPrefs.SetInt("SoldierSetSkin", 0);
        PlayerPrefs.SetInt("TankSetSkin", 0);
        PlayerPrefs.SetInt("FighterSetSkin", 0);
        SkinManager.SkinChange("soldier", 0);
        SkinManager.SkinChange("tank", 0);
        SkinManager.SkinChange("fighter", 0);
        CoinManager.GetCoin();
        closeWindow.SetActive(false);
        for (int i = 1; i < 18; i++)
        {
            string soldierName = "SoldierSkin" + i.ToString();
            string tankName = "TankSkin" + i.ToString();
            string fighterName = "FighterSkin" + i.ToString();
            PlayerPrefs.SetInt(soldierName, 0);
            PlayerPrefs.SetInt(tankName, 0);
            PlayerPrefs.SetInt(fighterName, 0);
        }
        
        SkinViewManager.DefaultSkinSet();
        SkinManager.Init();
    }
}
