using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    public static List<(int,int,bool,string)> stage;
    private static int[,] board;
    public int vertical;
    public int horizontal;
    private void Start()
    {
        board = new int[vertical, horizontal];
    }
    public static void Set(int x,int y,int mobColor)
    {
        board[x, y] = mobColor;
    }
    public static bool CanSet(int x,int y)
    {
        return board[x, y] == 0;
    }

}
