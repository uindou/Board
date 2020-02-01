using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    public static List<(int,int,bool,string)> stage;
    public GameObject[,] objs;
    private static int[,] board;
    public int vertical;
    public int horizontal;
    private void Start()
    {
        board = new int[vertical, horizontal];
        objs = new GameObject[vertical, horizontal];
    }
    public static void Set(int x,int y,int mobColor)
    {
        board[x, y] = mobColor;
    }
    public static bool CanSet(int x,int y)
    {
        return board[x, y] == 0;
    }
    public static List<(int, int, bool, string)> makeStage()
    {
        stage = new List<(int, int, bool, string)>();
        stage.Add((4, 3, true, "Soldier"));
        return stage;
    }

    void Start()
    {
        objSearch();
    }

    void objSearch()
    {
        for(int i = 0; i < vertical; i++)
        {
            for(int j = 0; j < horizontal; j++)
            {

            }
        }
    }
}
