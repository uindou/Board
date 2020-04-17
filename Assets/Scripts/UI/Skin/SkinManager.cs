using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    public static Transform SkinData;
    public static List<(string, Sprite, int, bool)> SoldierSkin;
    public static List<(string, Sprite, int, bool)> TankSkin;
    public static List<(string, Sprite, int, bool)> FighterSkin;
    private void Awake()
    {
        SkinData = this.gameObject.transform;
        SetInit();
        Init();
        
    }
    public static void Init()
    {
        Debug.Log("load init");
        SoldierInit();
        TankInit();
        PlainInit();
    }
    private void SetInit()
    {
        PlayerPrefs.SetInt("SoldierSkin0",1);
        PlayerPrefs.SetInt("TankSkin0", 1);
        PlayerPrefs.SetInt("FighterSkin0", 1);
    }
    public static void SoldierInit()
    {
        int[] Prices = new int[] { 0, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 200, 200, 200, 200, 200, 200 };
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
    public static void TankInit()
    {
        int[] Prices = new int[] { 0, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 200, 200, 200, 200, 200, 200 };
        TankSkin = new List<(string, Sprite, int, bool)>();
        Transform Tank = SkinData.GetChild(1);
        int skinCount = Tank.childCount;
        for (int i = 0; i < skinCount; i++)
        {
            Transform thisTank = Tank.GetChild(i);
            TankSkin.Add((thisTank.name,
                thisTank.GetComponent<SpriteRenderer>().sprite,
                Prices[i],
                PlayerPrefs.GetInt("TankSkin" + i.ToString(), 0) == 1));
        }
    }
    public static void PlainInit()
    {
        int[] Prices = new int[] { 0, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 200, 200, 200, 200, 200, 200 };
        FighterSkin = new List<(string, Sprite, int, bool)>();
        Transform Plain = SkinData.GetChild(2);
        int skinCount = Plain.childCount;
        for (int i = 0; i < skinCount; i++)
        {
            Transform thisPlain = Plain.GetChild(i);
            FighterSkin.Add((thisPlain.name,
                thisPlain.GetComponent<SpriteRenderer>().sprite,
                Prices[i],
                PlayerPrefs.GetInt("FighterSkin" + i.ToString(), 0) == 1));
        }
    }
    public static bool Available(string type,int index)
    {
        int coin = PlayerPrefs.GetInt("coin", 0);
        int price;
        if (type == "soldier")
        {
            var (_, _, p, _) = SoldierSkin[index];
            price = p;
        }else if(type == "tank")
        {
            var (_, _, p, _) = TankSkin[index];
            price = p;
        }
        else
        {
            var (_, _, p, _) = FighterSkin[index];
            price = p;
        }
        return coin >= price;
    }
    public static void Purchase(string type, int index)
    {
        int price;
        (string, Sprite, int, bool) Skin;
        if (type == "soldier")
        {
            Skin = SoldierSkin[index];
        }
        else if (type == "tank")
        {
            Skin = TankSkin[index];
        }
        else
        {
            Skin = FighterSkin[index];
        }
        var (a, b, c, _) = Skin;
        Debug.Log(Skin);
        price = c;
        int coin = PlayerPrefs.GetInt("coin", 0);
        Debug.Log(coin);
        PlayerPrefs.SetInt("coin", coin - price);
        Debug.Log(price);
        Skin = (a, b, c, true);
        if (type == "soldier")
        {
            SoldierSkin[index] = Skin;
            PlayerPrefs.SetInt("SoldierSkin" + index.ToString(), 1);
        }
        else if (type == "tank")
        {
            TankSkin[index] = Skin;
            PlayerPrefs.SetInt("TankSkin" + index.ToString(), 1);
        }
        else
        {
            FighterSkin[index] = Skin;
            PlayerPrefs.SetInt("FighterSkin" + index.ToString(), 1);
        }

    }
    public static void SkinChange(string type,int index)
    {
        switch (SkinViewManager.nowSkin)
        {
            case (int)SkinViewManager.skinType.soldier:
                PlayerPrefs.SetInt("SoldierSetSkin", index);
                var (a, b, _, _) = SoldierSkin[index];
                SkinViewManager.SkinSet(type, a, b, index);
                break;
            case (int)SkinViewManager.skinType.tank:
                PlayerPrefs.SetInt("TankSetSkin", index);
                var (c, d, _, _) = TankSkin[index];
                SkinViewManager.SkinSet(type, c, d, index);
                break;
            case (int)SkinViewManager.skinType.fighter:
                PlayerPrefs.SetInt("FighterSetSkin", index);
                var (e, f, _, _) = FighterSkin[index];
                SkinViewManager.SkinSet(type, e, f, index);
                break;

        }
    }

    public static bool BuyFlag(string type,int index)
    {
        if (type == "soldier")
        {
            var (_, _, _, flag) = SoldierSkin[index];
            return flag;
        }
        else if (type == "tank")
        {
            var (_, _, _, flag) = TankSkin[index];
            return flag;
        }
        else
        {
            var (_, _, _, flag) = FighterSkin[index];
            return flag;
        }
    }

}
