using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StageSelectManager : MonoBehaviour
{
    //public static List<Sprite> stages;
    private static List<int> stageNumbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    private static int Length;
    public static GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = GameObject.Find("Stages");
        Length = stageNumbers.Count;
        //Draw();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private static void Draw()
    {/*
        parent.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>().text = "Stage" + stageNumbers[9].ToString();
        parent.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Text>().text = "Stage" + stageNumbers[0].ToString();
        parent.transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<Text>().text = "Stage" + stageNumbers[1].ToString();
    */}

    public static void StageRotate(bool vector)
    {
        if (vector) {
            int movingNum = stageNumbers[Length -1];
            stageNumbers.RemoveAt(Length -1);
            stageNumbers.Insert(0,movingNum);

            //Sprite movingIm = stages[Length - 1];
           // stages.RemoveAt(Length - 1);
            //stages.Insert(0, movingIm); 
        }

        else if (!vector)
        {
            int movingNum = stageNumbers[0];
            stageNumbers.RemoveAt(0);
            stageNumbers.Insert(Length-1, movingNum);

            //Sprite movingIm = stages[0];
           // stages.RemoveAt(0);
           // stages.Insert(Length -1, movingIm);
        }
        
        Draw();
    }
}
