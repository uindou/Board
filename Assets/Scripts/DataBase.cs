﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using static gameManage;
public class DataBase : MonoBehaviour
{
    public static Sprite[] images = new Sprite[10];
    public static List<(int,int,bool,string)> stage;
    public static GameObject[,] objs;
    [SerializeField] static GameObject turnPhase;
    private static int[,] board;
    public static int vertical=7;
    public static int horizontal=5;
    public static bool selectFlug;
    public static (int, int) selectMove;
    public static bool moveFlug;
    public static (int, int) realMove;
    public static bool attackSelectFlug;
    public static (int, int) attackSelect;
    public static bool attackFlug;
    public static (int, int) realAttack;
    public static Sprite firstImage;
    public static situation receive;//他から変える
    public enum situation
    {
        select,
        move,
        attackselect,
        attack
    }
    public static void TurnChange()
    {
        turnPhase.GetComponent<TurnPhase>().turnUpdate(gameManage.turn);
        Debug.Log("changed");
    }
    public static void PhaseChange(bool phase)
    {
        turnPhase.GetComponent<TurnPhase>().PhaseUpdate(phase);
    }
    private void Start()
    {
        turnPhase = GameObject.Find("TurnPhase");
        board = new int[vertical, horizontal];
        objs = new GameObject[vertical, horizontal];
        objInit();
        ImageInit();
        firstImage = objs[0, 0].transform.GetChild(0).GetComponent<Image>().sprite;
    }
    public static void FlugInit()
    {
        selectFlug = false;
        moveFlug = false;
        attackSelectFlug = false;
        attackFlug = false;
    }
    public static List<GameObject> MyKoma(bool turn,bool mode)
    {
        List<GameObject> res = new List<GameObject>();
        int mobcolor = turn ? 2 : 1;
        for (int i = 0; i < vertical; i++)
        {
            for (int j = 0; j < horizontal; j++)
            {
                if (board[i, j] == mobcolor)
                {
                    GameObject obj = objs[i, j];
                    if (mode)
                    {
                        if (obj.GetComponent<interFace>().Movable().Any()) res.Add(obj);
                    }
                    else
                    {
                        if (obj.GetComponent<interFace>().Attackable().Any()) res.Add(obj);
                    }
                }
            }
        }
        return res;
    }
    private void ImageInit()
    {
        images[0] = GameObject.Find("Soldier").GetComponent<SpriteRenderer>().sprite;
        images[1] = GameObject.Find("Tank").GetComponent<SpriteRenderer>().sprite;
        images[2] = GameObject.Find("Heart").GetComponent<SpriteRenderer>().sprite;
        images[3] = GameObject.Find("BrokenHeart").GetComponent<SpriteRenderer>().sprite;
        images[4] = GameObject.Find("MoveSelection").GetComponent<SpriteRenderer>().sprite;
        images[5] = GameObject.Find("Transparent").GetComponent<SpriteRenderer>().sprite;
        images[6] = GameObject.Find("AttackSelection").GetComponent<SpriteRenderer>().sprite;
    }
    public static Sprite image(int i)
    {
        return images[i];
    }
    public static void Set(int x,int y,int mobColor)
    {
        board[x, y] = mobColor;//0→何もなし　1→相手？　2→自分？
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
    public static void GameEnd(bool turn)
    {
        turnPhase.GetComponent<TurnPhase>().GameEnd(turn);
    }

    public static bool NoKoma(bool turn)
    {
        int enemycolor = turn ? 1 : 2;
        foreach(int b in board)
        {
            if (b == enemycolor) return false;
        }
        return true;

    }
    public static bool GameOver(bool turn)
    {
        if (turn)
        {
            for(int i = 0; i< horizontal; i++)
            {
                if (board[0,i] == 1)
                {
                    return true;
                }
            }
        }
        else
        {
            for(int i = 0; i < horizontal; i++)
            {
                if (board[vertical-1, i] == 2)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public static bool CanAttack(int x, int y,bool enemycolor)
    {
        int mobcolor = enemycolor ? 2 : 1;
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
        stage.Add((1, 1, true, "Soldier"));
        stage.Add((1, 2, true, "Tank"));
        stage.Add((1, 3, true, "Tank"));
        stage.Add((1, 4, true, "Tank"));
        stage.Add((1, 5, true, "Soldier"));

        stage.Add((7, 1, false, "Soldier"));
        stage.Add((7, 2, false, "Tank"));
        stage.Add((7, 3, false, "Tank"));
        stage.Add((7, 4, false, "Tank"));
        stage.Add((7, 5, false, "Soldier"));
        return stage;
    }
    public static bool CantAttack(bool turn)
    {
        int mobcolor = turn ? 2 : 1;
        int enemycolor = turn ? 1 : 2;
        for(int i = 0; i < vertical; i++)
        {
            for(int j = 0; j < horizontal; j++)
            {
                if (board[i, j] == mobcolor)
                {
                    GameObject obj = objs[i, j];
                    List<(int, int)> res = obj.GetComponent<interFace>().Attackable();
                    foreach((int,int) T in res)
                    {
                        var (k,l) = T;
                        if (board[k, l] == enemycolor) return false;
                    }
                }
            }
        }
        return true;
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
    public static void Reset(situation sit)
    {
        bool flug = false;
        (int, int) move = (0,0);
        (flug, move) = RequestAdmin(flug, move, sit, false);
    }
  
    public static bool Question(situation sit)
    {
        bool flug = true;
        (int, int) move = (0, 0);
        (flug, move) = RequestAdmin(flug,move,sit,true);
        return flug;
    }
    public static void Request(int i,int j,situation sit)
    {
        bool flug = true;
        (int, int) move = (0,0);

        (flug,move)=RequestAdmin(flug,move,sit,true);
        if (!flug)
        {
            RequestAdmin(true,(i,j),sit,false);
        }
    }
    
    public static(bool,(int, int)) RequestAdmin(bool flug,(int,int) move,situation sit,bool isOutput)
        //isOutputがtrue→その時のムーブとフラグを返す　false→フラグとムーブをセットする
    {
        receive = sit;
        switch(receive)
        {
            case situation.select:
                if(isOutput) return (selectFlug, selectMove);
                else
                {
                    selectFlug = flug;
                    selectMove = move;
                    return (flug, move);
                }
            case situation.move:
                if (isOutput) return (moveFlug, realMove);
                else
                {
                    moveFlug = flug;
                    realMove = move;
                    return (flug, move);
                }
            case situation.attackselect:
                if (isOutput) return (attackSelectFlug, attackSelect);
                else
                {
                    attackSelectFlug = flug;
                    attackSelect = move;
                    return (flug, move);
                }
            case situation.attack:
                if (isOutput) return (attackFlug, realAttack);
                else
                {
                    attackFlug = flug;
                    realAttack = move;
                    return (flug, move);
                }
            default:
                return (flug,move);
        }
    }
}
