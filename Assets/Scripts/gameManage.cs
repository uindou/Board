using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataBase;
using static gameManage;

public class gameManage : MonoBehaviour
{
    [SerializeField]State state;
    public static situation receiveMode;
    public enum situation
    {
        select,
        move
    }
    /*public static void requestEnqueue(GameObject obj)
    {
        Debug.Log("ゲームマネージャー起動");
        switch (obj.GetComponent<interFace>().GetName())
        {
            case "Soldier":
                Debug.Log("分岐");
                List<(int, int)> area = obj.GetComponent<interFace>().Movable();
                foreach ((int, int) T in area)
                {
                    Debug.Log("送信");
                    var (i, j) = T;
                    GameObject obj1 = GameObject.Find("Grid(" + i + "," + j + ")");
                    Debug.Log("送信2");
                    Debug.Log(i);
                    Debug.Log(j);
                    obj1.GetComponent<clickReceiver>().Flash();

                }
                break;
            default:
                break;
        }
    }*/
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
                clickReceiver res = obj.GetComponent<clickReceiver>();
                if (res.IsMoveRange())
                {
                    int i3, j3;
                    (i3,j3)=DataBase.selectMove;
                    GameObject obj1 = DataBase.objs[i3,j3];
                    Debug.Log(obj1+","+obj);
                    List<(int, int)> area = obj1.GetComponent<interFace>().Movable();
                    foreach ((int, int) T in area)
                    {
                        var (i2, j2) = T;
                        GameObject obj2 = DataBase.objs[i2 - 1, j2 - 1];
                        obj1.GetComponent<clickReceiver>().StopFlash();
                    }
                    Vector3 c = obj.transform.position;
                    obj.transform.position = obj1.transform.position;
                    obj1.transform.position = c;
                    Debug.Log("変更完了");


                }
                break;
            default:
                break;


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
            Debug.Log("select stateまで来た");
            int i, j;
            (i, j) = DataBase.selectMove;
            GameObject obj = DataBase.objs[i, j];
            List<(int, int)> area = obj.GetComponent<interFace>().Movable();
            foreach ((int, int) T in area)
            {
                var (i2, j2) = T;
                Debug.Log(i2+","+j2);
                GameObject obj1 = DataBase.objs[i2-1, j2-1];
                obj1.GetComponent<clickReceiver>().Flash();
            }
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