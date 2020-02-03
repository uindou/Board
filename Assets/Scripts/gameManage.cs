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

    public enum situation
    {
        select,
        move,
        attackselect
    }
    public static void requestEnqueue(GameObject obj)
    {
        switch (receiveMode)
        {
            case situation.select:
                if (obj.GetComponent<interFace>() != null)
                {
                    int s, t;
                    (s, t) = DataBase.objSearch(obj);
                    DataBase.SelectRequest(s, t);
                }
                break;
            case situation.move:
                if(obj.GetComponent<interFace>() != null)
                {
                    int i3, j3;
                    (i3, j3) = DataBase.selectMove;
                    GameObject obj1 = DataBase.objs[i3, j3];
                    FlashControl(obj1,false);
                    int s, t;
                    (s, t) = DataBase.objSearch(obj);
                    DataBase.SelectReset();
                    DataBase.SelectRequest(s, t);
                    moveReset = true;
                    break;
                }
                else if (obj.GetComponent<clickReceiver>().IsMoveRange())
                {
                    int s, t;
                    (s, t) = DataBase.objSearch(obj);
                    DataBase.MoveRequest(s, t);
                }
                break;
            case situation.attackselect:
                if (obj.GetComponent<interFace>() != null)
                {
                    int i3, j3;
                    (i3, j3) = DataBase.selectMove;
                    GameObject obj1 = DataBase.objs[i3, j3];
                    AttackFlash(obj1, true);
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
            GameObject obj1 = DataBase.objs[i2 - 1, j2 - 1];
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
            GameObject obj1 = DataBase.objs[i2-1, j2-1];
            if (obj1.GetComponent<interFace>() != null /*& obj1.GetComponent<interFace>().IsTeam() == false*/)
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
    private void Start()
    {
        state = new Start();
        receiveMode = situation.select;
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
        return new Select();
    }
}

public class Select : State
{
    public State Execute()
    {
        if (DataBase.Select())
        {
            int i, j;
            (i, j) = DataBase.selectMove;
            GameObject obj = DataBase.objs[i, j];
            gameManage.FlashControl(obj, true);
            gameManage.receiveMode = situation.move;
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
        if (DataBase.Move())
        {
            int i3, j3;
            int i4, j4;
            (i3, j3) = DataBase.selectMove;
            (i4, j4) = DataBase.move;
            GameObject obj = DataBase.objs[i4, j4];
            GameObject obj1 = DataBase.objs[i3, j3];
            gameManage.FlashControl(obj1, false);
            Vector3 c = obj.transform.position;
            obj.transform.position = obj1.transform.position;
            obj1.transform.position = c;
            gameManage.receiveMode = situation.attackselect;
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
        return this;
    }
}
public class Final : State
{
    public State Execute()
    {
        return this;
    }
}