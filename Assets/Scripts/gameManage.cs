using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataBase;
using static gameManage;

public class gameManage : MonoBehaviour
{
    [SerializeField]State state;
    public static situation receiveMode;
    public static bool moveReset;
    public static bool attackReset;
    public static bool turn;

    public enum situation
    {
        select,
        move,
        attackselect,
        attack,
        free
    }
    public static void requestEnqueue(GameObject obj)
    {
        switch (receiveMode)
        {
            case situation.select:
                if (obj.GetComponent<interFace>() != null)
                    
                {
                    if(obj.GetComponent<interFace>().IsTeam() == turn)
                    {
                        int s, t;
                        (s, t) = DataBase.objSearch(obj);
                        DataBase.Request(s, t, DataBase.situation.select);
                    }
                    
                }
                break;

            case situation.move:
                if(obj.GetComponent<interFace>() != null)
                {
                    if(obj.GetComponent<interFace>().IsTeam() == turn)
                    {
                        int i3, j3;
                        (i3, j3) = DataBase.selectMove;
                        GameObject obj1 = DataBase.objs[i3, j3];
                        FlashControl(obj1, false);
                        int s, t;
                        (s, t) = DataBase.objSearch(obj);
                        DataBase.Reset(DataBase.situation.select);
                        DataBase.Request(s, t, DataBase.situation.select);
                        moveReset = true;
                    }
                    break;

                }
                else if (obj.GetComponent<clickReceiver>().IsRange())
                {
                    int s, t;
                    (s, t) = DataBase.objSearch(obj);
                    DataBase.Request(s, t, DataBase.situation.move);
                }
                break;

            case situation.attackselect:
                if (obj.GetComponent<interFace>() != null)
                {
                    if (obj.GetComponent<interFace>().IsTeam() == turn)
                    {
                        int s, t;
                        (s, t) = DataBase.objSearch(obj);
                        DataBase.Request(s, t, DataBase.situation.attackselect);
                    }    
                }
                break;

            case situation.attack:
                if (obj.GetComponent<interFace>() != null)
                {
                    if (obj.GetComponent<interFace>().IsTeam() == turn)
                    {
                        int i3, j3;
                        (i3, j3) = DataBase.attackSelect;
                        GameObject obj1 = DataBase.objs[i3, j3];
                        AttackFlash(obj1, false);
                        int s, t;
                        (s, t) = DataBase.objSearch(obj);
                        DataBase.Reset(DataBase.situation.attackselect);
                        DataBase.Request(s, t, DataBase.situation.attackselect);
                        attackReset = true;
                        
                    }

                    else if (obj.GetComponent<clickReceiver>().IsRange())
                    {
                        int s, t;
                        (s, t) = DataBase.objSearch(obj);
                        DataBase.Request(s, t, DataBase.situation.attack);
                    }
                    break;
                }
                break;

            default:
                break;


        }
    }
    public static void FlashControl(GameObject obj,bool isflash)
    {
        List<(int, int)> area = obj.GetComponent<interFace>().Movable();
        foreach ((int, int) T in area)
        {
            var (i2, j2) = T;
            GameObject obj1 = DataBase.objs[i2, j2];
            if (isflash)
            {
                obj1.GetComponent<clickReceiver>().Flash();
            }
            else
            {
                obj1.GetComponent<clickReceiver>().StopFlash();
            }
        }
    }
    public static void AttackFlash(GameObject obj,bool isflash)
    {
        List<(int, int)> area = obj.GetComponent<interFace>().Attackable();
        foreach ((int, int) T in area)
        {
            var (i2, j2) = T;
            GameObject obj1 = DataBase.objs[i2, j2];
            if (obj1.GetComponent<interFace>() != null)
            {
                if (obj1.GetComponent<interFace>().IsTeam() != turn)
                {
                    if (isflash)
                    {
                        obj1.GetComponent<clickReceiver>().Flash();
                    }
                    else
                    {
                        obj1.GetComponent<clickReceiver>().StopFlash();
                    }
                }
            }
        }
    }
    private void Start()
    {
        state = new Start();
        receiveMode = situation.select;
        turn = false;
    }
    private void Update()
    {
        state = state.Execute();
    }
}

public interface State
{
    State Execute();
}

public class Start : State
{
    public State Execute()
    {
        DataBase.FlugInit();
        gameManage.receiveMode = gameManage.situation.select;
        return new Select();
    }
}

public class Select : State
{
    public State Execute()
    {
        if (DataBase.selectFlug)
        {
            int i, j;
            (i, j) = DataBase.selectMove;
            GameObject obj = DataBase.objs[i, j];
            gameManage.FlashControl(obj, true);
            gameManage.receiveMode = gameManage.situation.move;
            return new Move();
        }
        else
        {
            return this;
        }
    }
}

public class Move:State{
    public State Execute()
    {
        if (DataBase.moveFlug)
        {
            int i3, j3;
            int i4, j4;
            (i3, j3) = DataBase.selectMove;
            (i4, j4) = DataBase.realMove;
            GameObject obj = DataBase.objs[i4, j4];
            GameObject obj1 = DataBase.objs[i3, j3];
            gameManage.FlashControl(obj1, false);

            Vector3 c = obj.transform.position;
            obj.transform.position = obj1.transform.position;
            obj1.transform.position = c;
            DataBase.objs[i3, j3] = obj;
            DataBase.objs[i4, j4] = obj1;
            obj1.GetComponent<interFace>().Move(i4,j4);

            gameManage.receiveMode = gameManage.situation.attackselect;
            return new AttackSelect();
        }else if(gameManage.moveReset){
            gameManage.moveReset = false;
            return new Select();
        }
        else
        {
            return this;
        }
    }
}
public class AttackSelect : State
{
    public State Execute()
    {
        if (DataBase.attackSelectFlug)
        {
            int i3, j3;
            (i3, j3) = DataBase.attackSelect;
            GameObject obj1 = DataBase.objs[i3, j3];
            AttackFlash(obj1, true);
            gameManage.receiveMode = gameManage.situation.attack;
            return new Attack();
        }
        else
        {
            return this;
        }
    }
}
public class Attack : State
{
    public State Execute()
    {
        if (DataBase.attackFlug)
        {
            int i3, j3;
            int i4, j4;
            (i3, j3) = DataBase.attackSelect;
            (i4, j4) = DataBase.realAttack;
            GameObject obj = DataBase.objs[i4, j4];
            GameObject obj1 = DataBase.objs[i3, j3];
            gameManage.AttackFlash(obj1, false);
            //obj.GetComponent<interFace>().AddDamage(obj1.GetComponent<interFace>().Power());

            gameManage.receiveMode = gameManage.situation.free;
            return new Final();
        }
        else if (gameManage.attackReset)
        {
            gameManage.attackReset = false;
            return new AttackSelect();
        }
        else
        {
            return this;
        }
    }
}
public class Final : State
{
    public State Execute()
    {
        gameManage.turn = !gameManage.turn;
        return new Start();
    }
}