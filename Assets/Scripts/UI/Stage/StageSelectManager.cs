using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class StageSelectManager : MonoBehaviour
{
    private static List<Sprite> stages;
    public static List<int> stageNumbers = new List<int>() { 1, 2, 3, 4, 5};
    public static List<int> AIstageNumbers;
    private static int Length;

    public static bool isBoardChange;
    
    public static GameObject stageImageParent;
    public static Transform PParent;
    public static Transform AIParent;
    public static Transform Left;
    public static Transform Center;
    public static Transform Right;
    public static Transform AILeft;
    public static Transform AICenter;
    public static Transform AIRight;

    public GameObject leftImage;
    public GameObject leftArrow;
    public GameObject rightImage;
    public GameObject rightArrow;

    public GameObject leftImage2;
    public GameObject leftArrow2;
    public GameObject rightImage2;
    public GameObject rightArrow2;

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
    public static bool[,,] AIchangeFlag;
    // Start is called before the first frame update
    void Awake()
    {
        isBoardChange = false;
        PlayerStage = GameObject.Find("PlayerStages");
        AIStage = GameObject.Find("AIStages");
        Debug.Log(PlayerStage);
        stageNumbers = new List<int>() { 1, 2, 3, 4, 5,6,7,8,9,10 };
        AIstageNumbers = new List<int>() { 1, 2, 3, 4, 5,6,7,8,9,10 };
        Length = stageNumbers.Count;
        PParents = GameObject.Find("StageSelectWindow");
        AIParents = GameObject.Find("AIStageSelectWindow");
        PParent = PParents.transform.GetChild(1);
        AIParent = AIParents.transform.GetChild(1);
        changeFlag = new bool[Length, DataBase.vertical, DataBase.horizontal];
        AIchangeFlag = new bool[Length, DataBase.vertical, DataBase.horizontal];
        for (int i = 0; i < Length; i++)
        {
            for (int j = 0; i < DataBase.vertical; i++)
            {
                for (int k = 0; j < DataBase.horizontal; j++)
                {
                    changeFlag[i, j, k] = false;
                    AIchangeFlag[i, j, k] = false;
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
        if (PParents.activeSelf)
        {
            

            if (stageNumbers[0] == 1)
            {
                leftImage2.SetActive(false);
                leftArrow2.SetActive(false);

                rightImage2.SetActive(true);
                rightArrow2.SetActive(true);
            }
            else if (stageNumbers[0] == 10)
            {
                leftImage2.SetActive(true);
                leftArrow2.SetActive(true);

                rightImage2.SetActive(false);
                rightArrow2.SetActive(false);
            }
            else
            {
                leftImage2.SetActive(true);
                leftArrow2.SetActive(true);

                rightImage2.SetActive(true);
                rightArrow2.SetActive(true);
            }
        }
        else
        {
            if (AIstageNumbers[0] == 1)
            {
                leftImage.SetActive(false);
                leftArrow.SetActive(false);

                rightImage.SetActive(true);
                rightArrow.SetActive(true);
            }
            else if (AIstageNumbers[0] == 10)
            {
                leftImage.SetActive(true);
                leftArrow.SetActive(true);

                rightImage.SetActive(false);
                rightArrow.SetActive(false);
            }
            else
            {
                leftImage.SetActive(true);
                leftArrow.SetActive(true);

                rightImage.SetActive(true);
                rightArrow.SetActive(true);
            }
        }

    }
    public static void MyInit()
    {
        if (PParents.activeSelf)
        {
            PlayerStage.transform.GetChild(1).GetChild(1).gameObject.GetComponent<Text>().text = "Field" + stageNumbers[0].ToString();
            Debug.Log("draw");
            StageInit(stageNumbers[Length - 1], Left,false);
            StageInit(stageNumbers[0], Center,false);
            StageInit(stageNumbers[1], Right,false);

        }
        else if (AIParents.activeSelf)
        {
            AIStage.transform.GetChild(1).GetChild(1).gameObject.GetComponent<Text>().text = "Field" + AIstageNumbers[0].ToString();
            Debug.Log("draw");
            StageInit(AIstageNumbers[Length - 1], AILeft,true);
            StageInit(AIstageNumbers[0], AICenter,true);
            StageInit(AIstageNumbers[1], AIRight,true);
        }
        else
        {
            Debug.Log("image draw failed");
        }
    }
    public static void Draw()
    {
        if (PParents.activeSelf)
        {
            PlayerStage.transform.GetChild(1).GetChild(1).gameObject.GetComponent<Text>().text = "Stage" + stageNumbers[0].ToString();
            Debug.Log("draw");
            StageDraw(stageNumbers[Length - 1], Left,false);
            StageDraw(stageNumbers[0], Center,false);
            StageDraw(stageNumbers[1], Right,false);
            
        }else if (AIParents.activeSelf)
        {
            AIStage.transform.GetChild(1).GetChild(1).gameObject.GetComponent<Text>().text = "Stage" + AIstageNumbers[0].ToString();
            Debug.Log("draw");
            StageDraw(AIstageNumbers[Length - 1], AILeft,true);
            StageDraw(AIstageNumbers[0], AICenter,true);
            StageDraw(AIstageNumbers[1], AIRight,true);
        }
        else
        {
            Debug.Log("image draw failed");
        }
        
    }
    public static void StageInit(int stage, Transform Parent,bool isAI)
    {
        List<(int, int, bool, string)> res = DataBase.ForInitMakeStage(stage);
        for (int i = 0; i < DataBase.vertical; i++)
        {
            for (int j = 0; j < DataBase.horizontal; j++)
            {
                Transform obj = Parent.GetChild(i).GetChild(j);
                if (isAI)
                {
                    if (AIchangeFlag[stage - 1, i, j])
                    {
                        obj.Rotate(0, 0, 180f);
                        AIchangeFlag[stage - 1, i, j] = false;
                    }
                }
                else
                {
                    if (changeFlag[stage - 1, i, j])
                    {
                        obj.Rotate(0, 0, 180f);
                        changeFlag[stage - 1, i, j] = false;
                    }
                }
                obj.GetChild(0).GetComponent<Image>().sprite = DefaultSkin;
            }
        }
    }
    public static void StageDraw(int stage, Transform Parent,bool isAI)
    {
        List<(int, int, bool, string)> res = DataBase.ForInitMakeStage(stage);
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
                if (isAI)
                {
                    AIchangeFlag[stage - 1, i, j] = true;
                }
                else
                {
                    changeFlag[stage - 1, i, j] = true;
                }
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
        MyInit();
        if (vector) {
            if (PParents.activeSelf)
            {
                int movingNum = stageNumbers[Length - 1];
                stageNumbers.RemoveAt(Length - 1);
                stageNumbers.Insert(0, movingNum);
            }
            else
            {
                int movingNum = AIstageNumbers[Length - 1];
                AIstageNumbers.RemoveAt(Length - 1);
                AIstageNumbers.Insert(0, movingNum);
            }
            /*
            Sprite movingIm = stages[Length - 1];
            stages.RemoveAt(Length - 1);
            stages.Insert(0, movingIm);
            */
        }

        else if (!vector)
        {
            
            if (PParents.activeSelf)
            {
                int movingNum = stageNumbers[0];
                stageNumbers.RemoveAt(0);
                stageNumbers.Insert(Length - 1, movingNum);
               
                
            }
            else
            {
                int movingNum = AIstageNumbers[0];
                AIstageNumbers.RemoveAt(0);
                AIstageNumbers.Insert(Length - 1, movingNum);
            }

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
