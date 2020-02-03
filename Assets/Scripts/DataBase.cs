using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    public static List<(int,int,bool,string)> stage;
    public static GameObject[,] objs;
    private static int[,] board;
    public static int vertical=7;
    public static int horizontal=5;
    private static bool selectFlug;
    public static (int, int) selectMove;
    private static bool moveFlug;
    public static (int, int) move;
    private void Start()
    {
        selectFlug = false;
        board = new int[vertical, horizontal];
        objs = new GameObject[vertical, horizontal];
        objInit();
    }
    public static void Set(int x,int y,int mobColor)
    {
        board[x, y] = mobColor;
    }
    public static bool CanSet(int x,int y)
    {
        if (x >= 0 & x < vertical & y >= 0 & y < horizontal)
        {
            return board[x, y] == 0;
        }
        else
        {
            return false;
        }
    }
    public static bool CanAttack(int x, int y,bool enemycolor)
    {
        int mobcolor = enemycolor ? 1 : 0;
        if (x >= 0 & x < vertical & y >= 0 & y < horizontal)
        {
            return board[x, y] == mobcolor;
        }
        else
        {
            return false;
        }
    }
    public static List<(int, int, bool, string)> makeStage()
    {
        stage = new List<(int, int, bool, string)>();
        stage.Add((2, 2, true, "Soldier"));
        stage.Add((2, 3, true, "Soldier"));
        stage.Add((2, 4, true, "Soldier"));
        stage.Add((4, 3, false, "Soldier"));
        return stage;
    }
    public static (int, int) objSearch(GameObject obj)
    {
        for (int i = 0; i < vertical; i++)
        {
            for (int j = 0; j < horizontal; j++)
            {
                if (obj == objs[i, j])
                {
                    return (i, j);
                }
            }
        }
        return (1,1);
    }
            
    void objInit()
    {
        for(int i = 0; i < vertical; i++)
        {
            for(int j = 0; j < horizontal; j++)
            {
                objs[i,j] = GameObject.Find("Grid(" + i + "," + j + ")");
                if (objs[i, j] == null)
                {
                    Debug.Log("init failed"+i+","+j);
                }
            }
        }
        Debug.Log("Init succesed");
    }
    public static bool Select()
    {
        return selectFlug;
    }
    public static void SelectReset()//リクエスト
    {
        selectFlug = false;
    }
    public static void SelectRequest(int i,int j)//リクエストに対して、初めて置くときだけ許可
    {
        if (!selectFlug)
        {
            selectMove = (i, j);
            selectFlug = true;
        }
    }
    public static bool Move()
    {
        return moveFlug;
    }
    public static void MoveRequest(int i, int j)
    {
        if (!moveFlug)
        {
            move = (i, j);
            moveFlug = true;
        }
    }
}
