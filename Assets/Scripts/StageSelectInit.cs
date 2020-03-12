using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectInit : MonoBehaviour
{
    public static Transform Parent;
    public static Transform Left;
    public static Transform Center;
    public static Transform Right;
    public static GameObject Parents;
    public static Sprite SoldierSkin;
    public static Sprite TankSkin;
    public static Sprite FighterSkin;
    public static Sprite DefaultSkin;
    public static bool[,] changeFlag;

    public static void StageInit(int stage)
    {

        
        StageInitSub(stage, Center);
        
    }
    public static void StageInitSub(int stage,Transform Parent)
    {
        List<(int, int, bool, string)> res = DataBase.ForInitMakeStage(stage);
        for(int i = 0; i < DataBase.vertical;i++)
        {
            for(int j = 0; j < DataBase.horizontal;j++)
            {
                Debug.Log((i, j));
                Transform obj = Parent.GetChild(i).GetChild(j);
                if (changeFlag[i, j])
                {
                    obj.Rotate(0, 0, 180f);
                    changeFlag[i, j] = false;
                }
                obj.GetChild(0).GetComponent<Image>().sprite = DefaultSkin;


            }
        }
        foreach ((int, int, bool, string) T in res)
        {
            var (i, j, team, name) = T;
            int teamColor = team ? 2 : 1;
            i -= 1;
            j -= 1;
            Transform obj = Parent.GetChild(i).GetChild(j);
            if (team)
            {
                obj.Rotate(0, 0, 180f);
                changeFlag[i, j] = true;
            }
            switch (name)
            {
                case "Soldier":
                    obj.GetChild(0).GetComponent<Image>().sprite = SoldierSkin;
                    
                    break;

                case "Tank":
                    obj.GetChild(0).GetComponent<Image>().sprite = TankSkin;
                    break;
                case "PlainFighter":
                    obj.GetChild(0).GetComponent<Image>().sprite = FighterSkin;
                    
                    break;
                default:
                    break;
            }
        }
    }
    // Start is called before the first frame update

    private void Awake()
    {
        Parents = GameObject.Find("StageSelectWindow");
        Parent = Parents.transform.GetChild(1);
        changeFlag = new bool[DataBase.vertical, DataBase.horizontal];
        for(int i = 0; i < DataBase.vertical; i++)
        {
            for(int j = 0; j < DataBase.horizontal; j++)
            {
                changeFlag[i, j] = false;
            }
        }
    }
    void Start()
    {
        Parents.SetActive(false);
        Left = Parent.GetChild(0).GetChild(0);
        Center = Parent.GetChild(1).GetChild(0);
        Right = Parent.GetChild(2).GetChild(0);
        SoldierSkin = this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        TankSkin = this.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
        FighterSkin = this.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite;
        DefaultSkin = this.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
