using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinViewManager : MonoBehaviour
{
    public static GameObject currentSkin;
    public GameObject skinMatrix;
    public static int page;
    public static int nowSkin;
    public string charaName;
    public bool isStart;
    static Transform SkinWindow;
    public GameObject leftArrow;
    public GameObject rightArrow;

    // Start is called before the first frame update
    public enum skinType
    {
        soldier=0,
        tank=1,
        fighter=2
    } 
    private void Awake()
    {
        nowSkin = (int)skinType.soldier;
        isStart = false;
    }
    void Start()
    {
        currentSkin = GameObject.Find("CurrentSkin");
        if (currentSkin == null) Debug.Log("skin search failed");
        SkinWindow = GameObject.Find("SkinWindow").transform.GetChild(1).GetChild(2).GetChild(2);
        var (name,sprite,_,_) = SkinManager.SoldierSkin[PlayerPrefs.GetInt("SoldierSetSkin", 0)];
        SkinSet("soldier", name, sprite, PlayerPrefs.GetInt("SoldierSetSkin", 0));
        page = 1;
        SkinView();
        isStart = true;
    }

    public static void DefaultSkinSet()
    {
        string name = "";
        Sprite skin = null;
        switch (nowSkin)
        {
            case (int)skinType.soldier:
                (name, skin, _, _) = SkinManager.SoldierSkin[0];
                break;
            case (int)skinType.tank:
                (name, skin, _, _) = SkinManager.TankSkin[0];
                break;
            case (int)skinType.fighter:
                (name, skin, _, _) = SkinManager.FighterSkin[0];
                break;
            default:
                (name, skin, _, _) = SkinManager.SoldierSkin[0];
                break;
        }
        
        
        currentSkin.transform.GetChild(0).GetComponent<Image>().sprite = skin;
        currentSkin.transform.GetChild(1).GetComponent<Text>().text = name;
    }
    private void OnEnable()
    {
        if (isStart)
        {
            page = 1;
            SkinView();
            Debug.Log("onenable");
            switch (nowSkin)
            {
                case (int)skinType.soldier:
                    var (name, sprite, _, _) = SkinManager.SoldierSkin[PlayerPrefs.GetInt("SoldierSetSkin", 0)];
                    SkinSet("soldier", name, sprite, PlayerPrefs.GetInt("SoldierSetSkin", 0));
                    break;
                case (int)skinType.tank:
                    var (nam, sprit, _, _) = SkinManager.SoldierSkin[PlayerPrefs.GetInt("TankSetSkin", 0)];
                    SkinSet("tank", nam, sprit, PlayerPrefs.GetInt("TankSetSkin", 0));
                    break;
                case (int)skinType.fighter:
                    var (na, spri, _, _) = SkinManager.SoldierSkin[PlayerPrefs.GetInt("FighterSetSkin", 0)];
                    SkinSet("fighter", na, spri, PlayerPrefs.GetInt("FighterSetSkin", 0));
                    break;
            }
            SkinPageText.PageChange(page);
        }
    }
    public static void ModeChange(int mode)
    {
        nowSkin = mode;
        page = 1;
        Debug.Log("onenable");
        switch (nowSkin)
        {
            case (int)skinType.soldier:
                var (name, sprite, _, _) = SkinManager.SoldierSkin[PlayerPrefs.GetInt("SoldierSetSkin", 0)];
                SkinSet("soldier", name, sprite, PlayerPrefs.GetInt("SoldierSetSkin", 0));
                break;
            case (int)skinType.tank:
                var (nam, sprit, _, _) = SkinManager.TankSkin[PlayerPrefs.GetInt("TankSetSkin", 0)];
                SkinSet("tank", nam, sprit, PlayerPrefs.GetInt("TankSetSkin", 0));
                break;
            case (int)skinType.fighter:
                var (na, spri, _, _) = SkinManager.FighterSkin[PlayerPrefs.GetInt("FighterSetSkin", 0)];
                SkinSet("fighter", na, spri, PlayerPrefs.GetInt("FighterSetSkin", 0));
                break;
        }
        SkinPageText.PageChange(page);
    }
    
    // Update is called once per frame
    void Update()
    {
        SkinView();
        if (page == 1)
        {
            leftArrow.SetActive(false);
            rightArrow.SetActive(true);
        }
        else if (page == 3)
        {
            leftArrow.SetActive(true);
            rightArrow.SetActive(false);
        }
        else
        {
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);
        }
    }
    public static void SkinSet(string type,string name,Sprite skin,int index)
    {
        currentSkin.transform.GetChild(0).GetComponent<Image>().sprite = skin;
        currentSkin.transform.GetChild(1).GetComponent<Text>().text = name;
        if(index<page*6 & index>=page*6-6) SkinWindow.GetChild(index-(page-1)*6).GetChild(2).gameObject.SetActive(true);
        for(int i = 0; i < 6; i++)
        {
            if ((page-1)*6+i != index)
            {
                SkinWindow.GetChild(i).GetChild(2).gameObject.SetActive(false);
            }
        }
    }
    
    public void SkinView()
    {
        List<(string, Sprite, int, bool)> skinInfo = new List<(string, Sprite, int, bool)>();
        switch (nowSkin)
        {
            case (int)skinType.soldier:
                skinInfo = SkinManager.SoldierSkin.GetRange((page - 1) * 6, 6);
                break;
            case (int)skinType.tank:
                skinInfo = SkinManager.TankSkin.GetRange((page - 1) * 6, 6);
                break;
            case (int)skinType.fighter:
                skinInfo = SkinManager.FighterSkin.GetRange((page - 1) * 6, 6);
                break;
        }
        //表示する6要素をリストから選択
        

        //行列に各々表示
        for(int i=0;i<6; i++)
        {
            var (_, image, price, flug) = skinInfo[i];

            
            skinMatrix.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = image;
            skinMatrix.transform.GetChild(i).transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = price.ToString();

            //買ってたら"Parchased"を表示
            if (flug)
            {
                skinMatrix.transform.GetChild(i).transform.GetChild(1).gameObject.SetActive(false);
                //skinMatrix.transform.GetChild(i).transform.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                skinMatrix.transform.GetChild(i).transform.GetChild(1).gameObject.SetActive(true);
                //skinMatrix.transform.GetChild(i).transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }

    public static void pageInc(bool pgvc)
    {
        if (pgvc) page += 1;
        else page -= 1;
        if (page == 0) page = 3;
        if (page == 4) page = 1;
        SkinPageText.PageChange(page);

        switch (nowSkin)
        {
            case (int)skinType.soldier:
                var (name, sprite, _, _) = SkinManager.SoldierSkin[PlayerPrefs.GetInt("SoldierSetSkin", 0)];
                SkinSet("soldier", name, sprite, PlayerPrefs.GetInt("SoldierSetSkin", 0));
                break;
            case (int)skinType.tank:
                var (nam, sprit, _, _) = SkinManager.SoldierSkin[PlayerPrefs.GetInt("TankSetSkin", 0)];
                SkinSet("tank", nam, sprit, PlayerPrefs.GetInt("TankSetSkin", 0));
                break;
            case (int)skinType.fighter:
                var (na, spri, _, _) = SkinManager.SoldierSkin[PlayerPrefs.GetInt("FighterSetSkin", 0)];
                SkinSet("fighter", na, spri, PlayerPrefs.GetInt("FighterSetSkin", 0));
                break;
        }
    }
}
