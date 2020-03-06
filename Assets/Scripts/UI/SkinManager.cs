using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    Transform SkinData;
    List<(string, Sprite, int, bool)> SoldierSkin;
    List<(string, Sprite, int, bool)> TankSkin;
    List<(string, Sprite, int, bool)> PlainSkin;
    private void Awake()
    {
        SkinData = this.gameObject.transform;
        SetInit();
        SoldierInit();
        TankInit();
        PlainInit();
    }
    private void SetInit()
    {
        PlayerPrefs.SetInt("SoldierSkin0",1);
        PlayerPrefs.SetInt("TankSkin0", 1);
        PlayerPrefs.SetInt("PlainSkin0", 1);
    }
    private void SoldierInit()
    {
        int[] Prices = new int[] { 0, 100, 100, 100, 100, 100, 100, 100, 100, 100 };
        SoldierSkin = new List<(string, Sprite, int, bool)>();
        Transform Soldier = SkinData.GetChild(0);
        int skinCount = Soldier.childCount;
        for(int i = 0; i < skinCount; i++)
        {
            Transform thisSoldier = Soldier.GetChild(i);
            SoldierSkin.Add((thisSoldier.name,
                thisSoldier.GetComponent<SpriteRenderer>().sprite,
                Prices[i],
                PlayerPrefs.GetInt("SoldierSkin" + i.ToString(), 0) == 1));
            Debug.Log(SoldierSkin[i]);
        }
    }
    private void TankInit()
    {
        int[] Prices = new int[] { 0, 100, 100, 100, 100, 100, 100, 100, 100, 100 };
        TankSkin = new List<(string, Sprite, int, bool)>();
        Transform Tank = SkinData.GetChild(1);
        int skinCount = Tank.childCount;
        for (int i = 0; i < skinCount; i++)
        {
            Transform thisTank = Tank.GetChild(i);
            SoldierSkin.Add((thisTank.name,
                thisTank.GetComponent<SpriteRenderer>().sprite,
                Prices[i],
                PlayerPrefs.GetInt("TankSkin" + i.ToString(), 0) == 1));
        }
    }
    private void PlainInit()
    {
        int[] Prices = new int[] { 0, 100, 100, 100, 100, 100, 100, 100, 100, 100 };
        TankSkin = new List<(string, Sprite, int, bool)>();
        Transform Plain = SkinData.GetChild(2);
        int skinCount = Plain.childCount;
        for (int i = 0; i < skinCount; i++)
        {
            Transform thisPlain = Plain.GetChild(i);
            SoldierSkin.Add((thisPlain.name,
                thisPlain.GetComponent<SpriteRenderer>().sprite,
                Prices[i],
                PlayerPrefs.GetInt("PlainSkin" + i.ToString(), 0) == 1));
        }
    }
    public List<(string, Sprite, int, bool)> GetSoldier()
    {
        return SoldierSkin;
    }
    public List<(string, Sprite, int, bool)> GetTank()
    {
        return TankSkin;
    }
    public List<(string, Sprite, int, bool)> GetPlain()
    {
        return PlainSkin;
    }

}
