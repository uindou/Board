﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class StageSelectManager : MonoBehaviour
{
    private static List<Sprite> stages;
    private static List<int> stageNumbers = new List<int>() { 1, 2, 3, 4, 5};
    private static int Length;
    
    public static GameObject stageImageParent;
    public static Transform PParent;
    public static Transform AIParent;
    public static Transform Left;
    public static Transform Center;
    public static Transform Right;
    public static Transform AILeft;
    public static Transform AICenter;
    public static Transform AIRight;

    public static GameObject PlayerStage;
    public static GameObject AIStage;
    public static GameObject PParents;
    public static GameObject AIParents;
    public Transform ImageData;

    public static Sprite SoldierSkin;
    public static Sprite TankSkin;
    public static Sprite FighterSkin;
    public static Sprite DefaultSkin;
    public static bool[,,] changeFlag;
    // Start is called before the first frame update
    void Awake()
    {
        PlayerStage = GameObject.Find("PlayerStages");
        AIStage = GameObject.Find("AIStages");
        Debug.Log(PlayerStage);
        stageNumbers = new List<int>() { 1, 2, 3, 4, 5 };
        Length = stageNumbers.Count;
        PParents = GameObject.Find("StageSelectWindow");
        AIParents = GameObject.Find("AIStageSelectWindow");
        PParent = PParents.transform.GetChild(1);
        AIParent = AIParents.transform.GetChild(1);
        changeFlag = new bool[Length, DataBase.vertical, DataBase.horizontal];
        for (int i = 0; i < Length; i++)
        {
            for (int j = 0; i < DataBase.vertical; i++)
            {
                for (int k = 0; j < DataBase.horizontal; j++)
                {
                    changeFlag[i, j, k] = false;
                }
            }
        }

    }

    private void Start()
    {
        PParents.SetActive(false);
        AIParents.SetActive(false);
        Left = PParent.GetChild(0).GetChild(0);
        Center = PParent.GetChild(1).GetChild(0);
        Right = PParent.GetChild(2).GetChild(0);
        AILeft = AIParent.GetChild(0).GetChild(0);
        AICenter = AIParent.GetChild(1).GetChild(0);
        AIRight = AIParent.GetChild(2).GetChild(0);

        SoldierSkin = ImageData.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        TankSkin = ImageData.GetChild(1).GetComponent<SpriteRenderer>().sprite;
        FighterSkin = ImageData.GetChild(2).GetComponent<SpriteRenderer>().sprite;
        DefaultSkin = ImageData.GetChild(3).GetComponent<SpriteRenderer>().sprite;
    }
    // Update is called once per frame
    void Update()
    {

    }

    public static void Draw()
    {
        if (PParents.activeSelf)
        {
            PlayerStage.transform.GetChild(1).GetChild(1).gameObject.GetComponent<Text>().text = "Stage" + stageNumbers[0].ToString();
            Debug.Log("draw");
            StageInit(stageNumbers[Length - 1], Left);
            StageInit(stageNumbers[0], Center);
            StageInit(stageNumbers[1], Right);
        }else if (AIParents.activeSelf)
        {
            AIStage.transform.GetChild(1).GetChild(1).gameObject.GetComponent<Text>().text = "Stage" + stageNumbers[0].ToString();
            Debug.Log("draw");
            StageInit(stageNumbers[Length - 1], AILeft);
            StageInit(stageNumbers[0], AICenter);
            StageInit(stageNumbers[1], AIRight);
        }
        else
        {
            Debug.Log("image draw failed");
        }
        
    }
    public static void StageInit(int stage, Transform Parent)
    {
        List<(int, int, bool, string)> res = DataBase.ForInitMakeStage(stage);
        for (int i = 0; i < DataBase.vertical; i++)
        {
            for (int j = 0; j < DataBase.horizontal; j++)
            {
                Debug.Log((i, j));
                Transform obj = Parent.GetChild(i).GetChild(j);
                if (changeFlag[stage-1,i, j])
                {
                    obj.Rotate(0, 0, 180f);
                    changeFlag[stage-1,i, j] = false;
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
                changeFlag[stage-1,i, j] = true;
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

    public static void StageRotate(bool vector)
    {
        if (vector) {
            int movingNum = stageNumbers[Length -1];
            stageNumbers.RemoveAt(Length -1);
            stageNumbers.Insert(0,movingNum);

            /*
            Sprite movingIm = stages[Length - 1];
            stages.RemoveAt(Length - 1);
            stages.Insert(0, movingIm);
            */
        }

        else if (!vector)
        {
            int movingNum = stageNumbers[0];
            stageNumbers.RemoveAt(0);
            stageNumbers.Insert(Length-1, movingNum);

            /*
            Sprite movingIm = stages[0];
            stages.RemoveAt(0);
            stages.Insert(Length -1, movingIm);
            */
        }
        
        Draw();
    }

    private void OnEnable()
    {
    }
}
