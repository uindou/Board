using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using static gameManage;
public class DataBase : MonoBehaviour
{
    public static string SceneName;
    public static bool isOnline;
    public static bool winner = true;
    public static int bonusCoin;
    public static string preStage="Title";
    public static Sprite[] images;
    public static List<(int,int,bool,string)> stage;
    public static GameObject[,] objs;
    [SerializeField] static GameObject turnPhase;
    public static bool AImode;
    private static int[,] board;
    public static int vertical=9;
    public static int horizontal=7;
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
    public static bool bgmflug;
    
    public enum situation
    {
        select,
        move,
        attackselect,
        attack
    }

    public enum im
    {
        soldier = 0,
        soldierA1 = 8,
        soldierA2 = 11,
        tank = 1,
        tankA1 = 9,
        tankA2 = 12,
        tankA3 = 15,
        tankA4 = 16,
        plain = 14,
        plainA1 = 10,
        plainA2 = 13,
        heart = 2,
        brokenheart = 3,
        moveSelection = 4,
        attackSelection = 6,
        transParent = 5,
        simpleFrame = 7,
        exp1=17,
        exp2=18,
        exp3=19,
        exp4=20

    }

    private void Awake()
    {
        turnPhase = GameObject.Find("TurnPhase");
        vertical = 9;
        horizontal = 7;
        board = new int[vertical, horizontal];
        objs = new GameObject[vertical, horizontal];
        objInit();
        ImageInit();
        firstImage = objs[0, 0].transform.GetChild(0).GetComponent<Image>().sprite;
        switch (SceneName)
        {
            case "Stage1":
                AImode = false;
                break;
            case "Stage2":
                AImode = false;
                break;
            case "Stage3":
                AImode = false;
                break;
            case "AIStage1":
                AImode = true;
                break;
            case "AIStage2":
                AImode = true;
                break;
            case "AIStage3":
                AImode = true;
                break;
            default:
                break;
        }

    }
    public static void TurnChange()
    {
        turnPhase.GetComponent<TurnPhase>().turnUpdate(gameManage.turn);
    }
    public static void PhaseChange(bool phase)
    {
        turnPhase.GetComponent<TurnPhase>().PhaseUpdate(phase);
    }

    /*public static bool Jump(int x1,int y1,int x2,int y2)
    {
        int startx, starty, lastx, lasty;
        if (x1 < x2)
        {
            startx = x1;
            lastx = x2;
        }
        else
        {
            startx = x2;
            lastx = x1;
        }
        if (y1 < y2)
        {
            starty = y1;
            lasty = y2;
        }
        else
        {
            starty = y2;
            lasty = y1;
        }
        for(int x = startx+1; x < lastx; x++)
        {
            for(int y = starty + 1; y < lasty; y++)
            {
                Debug.Log((x, y));
                if (!CanSet(x, y))
                {
                    return false;
                }
            }
        }
        return true;
    }*/
    
    public static List<GameObject> FixedMyKoma(bool turn, bool mode)
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
                        if (obj.GetComponent<interFace>().AIMovable().Any()) res.Add(obj);
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
    /*---------------------------------------------INIT---------------------------------------------------------*/
    public static void FlugInit()
    {
        selectFlug = false;
        moveFlug = false;
        attackSelectFlug = false;
        attackFlug = false;
    }
    void objInit()
    {
        GameObject board = GameObject.Find("Board");
        for (int i = 0; i < vertical; i++)
        {
            for (int j = 0; j < horizontal; j++)
            {
                objs[i, j] = board.transform.GetChild(i).GetChild(j).gameObject;
                if (objs[i, j] == null)
                {
                    Debug.Log("init failed" + i + "," + j);
                }
            }
        }
        Debug.Log("Init succesed");
    }
    private void ImageInit()
    {
        images = new Sprite[System.Enum.GetNames(typeof(im)).Length];
        Transform imageParent = GameObject.Find("ImageDataBase").transform;
        Transform SoldierParent = imageParent.GetChild(0);
        Transform TankParent = imageParent.GetChild(1);
        Transform FighterParent = imageParent.GetChild(2);
        Transform Effects = imageParent.GetChild(3);
        Transform Others = imageParent.GetChild(4);
        Transform Explode = imageParent.GetChild(5);
        images[(int)im.soldier] = SoldierParent.GetChild(PlayerPrefs.GetInt("SoldierSetSkin", 0)).GetComponent<SpriteRenderer>().sprite;
        images[(int)im.tank] = TankParent.GetChild(PlayerPrefs.GetInt("TankSetSkin", 0)).GetComponent<SpriteRenderer>().sprite;
        images[(int)im.plain] = FighterParent.GetChild(PlayerPrefs.GetInt("FighterSetSkin", 0)).GetComponent<SpriteRenderer>().sprite;
        images[(int)im.soldierA1] = Effects.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        images[(int)im.soldierA2] = Effects.GetChild(1).GetComponent<SpriteRenderer>().sprite;
        images[(int)im.tankA1] = Effects.GetChild(2).GetComponent<SpriteRenderer>().sprite;
        images[(int)im.tankA2] = Effects.GetChild(3).GetComponent<SpriteRenderer>().sprite;
        images[(int)im.tankA3] = Effects.GetChild(4).GetComponent<SpriteRenderer>().sprite;
        images[(int)im.tankA4] = Effects.GetChild(5).GetComponent<SpriteRenderer>().sprite;
        images[(int)im.plainA1] = Effects.GetChild(6).GetComponent<SpriteRenderer>().sprite;
        images[(int)im.plainA2] = Effects.GetChild(7).GetComponent<SpriteRenderer>().sprite;
        images[(int)im.heart] = Others.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        images[(int)im.brokenheart] = Others.GetChild(1).GetComponent<SpriteRenderer>().sprite;
        images[(int)im.moveSelection] = Others.GetChild(2).GetComponent<SpriteRenderer>().sprite;
        images[(int)im.attackSelection] = Others.GetChild(3).GetComponent<SpriteRenderer>().sprite;
        images[(int)im.transParent] = Others.GetChild(4).GetComponent<SpriteRenderer>().sprite;
        images[(int)im.simpleFrame] = Others.GetChild(5).GetComponent<SpriteRenderer>().sprite;
        images[(int)im.exp1] = Explode.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        images[(int)im.exp2] = Explode.GetChild(1).GetComponent<SpriteRenderer>().sprite;
        images[(int)im.exp3] = Explode.GetChild(2).GetComponent<SpriteRenderer>().sprite;
        images[(int)im.exp4] = Explode.GetChild(3).GetComponent<SpriteRenderer>().sprite;
    }

    
   
    /*---------------------------------------------INIT---------------------------------------------------------*/
    public static Sprite image(int i)
    {
        return images[i];
    }
    public static void Set(int x,int y,int mobColor)
    {
        board[x, y] = mobColor;//0→何もなし　1→相手　2→自分
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
    public static int BoardInfo(int x,int y)
    {
        return board[x, y];
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
    public static bool EndWarn()
    {
        bool ret = false;
        List< (GameObject, (int, int) )> res = new List<(GameObject,(int,int))>();
        int mobcolor = turn ? 2 : 1;
        for (int i = 0; i < vertical; i++)
        {
            for (int j = 0; j < horizontal; j++)
            {
                if (board[i, j] == mobcolor)
                {
                    GameObject obj = objs[i, j];
                    foreach((int,int) T in obj.GetComponent<interFace>().Movable())
                    {
                        var (x, y) = T;
                        if (x == 0)
                        {
                            Debug.Log("Danger!!");
                            ret = true;
                            DangerForAI.AddDanger(T, (i,j), obj);
                        }
                    }
                }
            }
        }
        return ret;
    }
    /*---------------------------------------------------MAKE STAGE-----------------------------------------------*/
    public static List<(int, int, bool, string)> makeStage()
    {
        stage = new List<(int, int, bool, string)>();
        switch (SceneName)
        {
            case "Stage1":
                return Stage1MakeStage();
            case "Stage2":
                return Stage2MakeStage();
            case "Stage3":
                return Stage3MakeStage();
            case "AIStage1":
                return Stage1MakeStage();
            case "AIStage2":
                return Stage2MakeStage();
            case "AIStage3":
                return Stage3MakeStage();
            default:
                return Stage1MakeStage();
        }
    }
    public static List<(int, int, bool, string)> ForInitMakeStage(int stageNum)
    {
        stage = new List<(int, int, bool, string)>();
        switch (stageNum)
        {
            case 1:
                return Stage1MakeStage();
            case 2:
                return Stage2MakeStage();
            case 3:
                return Stage3MakeStage();
            default:
                return Stage1MakeStage();
        }
    }
    public static List<(int, int, bool, string)> Stage1MakeStage()
    {
        stage = new List<(int, int, bool, string)>();
        stage.Add((1, 1, true, "PlainFighter"));
        stage.Add((2, 1, true, "Soldier"));
        stage.Add((1, 2, true, "Soldier"));
        stage.Add((1, 3, true, "Tank"));
        stage.Add((1, 4, true, "Tank"));
        stage.Add((1, 5, true, "Tank"));
        stage.Add((1, 6, true, "Soldier"));
        stage.Add((2, 7, true, "Soldier"));
        stage.Add((1, 7, true, "PlainFighter"));

        stage.Add((9, 1, false, "PlainFighter"));
        stage.Add((8, 1, false, "Soldier"));
        stage.Add((9, 2, false, "Soldier"));
        stage.Add((9, 3, false, "Tank"));
        stage.Add((9, 4, false, "Tank"));
        stage.Add((9, 5, false, "Tank"));
        stage.Add((9, 6, false, "Soldier"));
        stage.Add((8, 7, false, "Soldier"));
        stage.Add((9, 7, false, "PlainFighter"));
        return stage;
    }
    public static List<(int, int, bool, string)> Stage2MakeStage()
    {
        stage = new List<(int, int, bool, string)>();
        stage.Add((1, 2, true, "Soldier"));
        stage.Add((2, 3, true, "Tank"));
        stage.Add((1, 3, true, "PlainFighter"));
        stage.Add((1, 4, true, "PlainFighter"));
        stage.Add((2, 4, true, "PlainFighter"));
        stage.Add((1, 5, true, "PlainFighter"));
        stage.Add((2, 5, true, "Tank"));
        stage.Add((1, 6, true, "Soldier"));

        stage.Add((9, 2, false, "Soldier"));
        stage.Add((8, 3, false, "Tank"));
        stage.Add((9, 3, false, "PlainFighter"));
        stage.Add((9, 4, false, "PlainFighter"));
        stage.Add((8, 4, false, "PlainFighter"));
        stage.Add((9, 5, false, "PlainFighter"));
        stage.Add((8, 5, false, "Tank"));
        stage.Add((9, 6, false, "Soldier"));
        return stage;
    }
    public static List<(int, int, bool, string)> Stage3MakeStage()
    {
        stage = new List<(int, int, bool, string)>();
        stage.Add((1, 1, true, "PlainFighter"));
        stage.Add((2, 1, true, "PlainFighter"));
        stage.Add((1, 2, true, "PlainFighter"));
        stage.Add((1, 3, true, "PlainFighter"));
        stage.Add((1, 4, true, "PlainFighter"));
        stage.Add((1, 5, true, "PlainFighter"));
        stage.Add((1, 6, true, "PlainFighter"));
        stage.Add((2, 7, true, "PlainFighter"));
        stage.Add((1, 7, true, "PlainFighter"));

        stage.Add((9, 1, false, "PlainFighter"));
        stage.Add((8, 1, false, "PlainFighter"));
        stage.Add((9, 2, false, "PlainFighter"));
        stage.Add((9, 3, false, "PlainFighter"));
        stage.Add((9, 4, false, "PlainFighter"));
        stage.Add((9, 5, false, "PlainFighter"));
        stage.Add((9, 6, false, "PlainFighter"));
        stage.Add((8, 7, false, "PlainFighter"));
        stage.Add((9, 7, false, "PlainFighter"));
        return stage;
    }
    
    /*---------------------------------------------------MAKE STAGE-----------------------------------------------*/
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
            
    
    /*--------------------------------------------Request---------------------------------------------------------*/
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
    /*--------------------------------------------Request---------------------------------------------------------*/
}
