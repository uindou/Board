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
        CoinManager.GetCoin();
        closeWindow.SetActive(false);
        PlayerPrefs.SetInt("SoldierSkin1", 0);
        PlayerPrefs.SetInt("SoldierSkin2", 0);
        PlayerPrefs.SetInt("SoldierSkin3", 0);
        PlayerPrefs.SetInt("SoldierSkin4", 0);
        PlayerPrefs.SetInt("SoldierSkin5", 0);
        SkinManager.Init();
    }
}
