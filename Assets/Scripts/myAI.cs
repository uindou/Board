using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using static gameManage;
using System.Linq;

public class myAI : MonoBehaviour
{
    private enum komaNum
    {
        empty,
        soldier,
        tank
    }
    private (((int,int),(int,int)), ((int, int), (int, int))) MiniMaxMain(int readTurn,int[,] board)
    {
        return (((0, 0), (0, 0)), ((0, 0), (0, 0)));
    }
    private async static void RandomAI()
    {
        List<GameObject> res = DataBase.MyKoma(gameManage.turn,true);
        System.Random r1 = new System.Random();
        GameObject obj = res[r1.Next(0, res.Count() - 1)];

        List<(int, int)> moveRange = obj.GetComponent<interFace>().Movable();
        var (i, j) = moveRange[r1.Next(0, moveRange.Count() - 1)];
        GameObject obj1 = DataBase.objs[i, j];

        await Task.Delay(1000);
        gameManage.requestEnqueue(obj);
        await Task.Delay(1000);
        gameManage.requestEnqueue(obj1);
        await Task.Delay(100);
        if (gameManage.receiveMode != gameManage.situation.attackselect)
        {
            return;
        }
        else
        {
            List<GameObject> res1 = DataBase.MyKoma(gameManage.turn, false);
            GameObject obj2 = res1[r1.Next(0, res1.Count() - 1)];
            List<(int, int)> attackRange = obj2.GetComponent<interFace>().Attackable();
            System.Random r2 = new System.Random();
            var (k, l) = attackRange[r2.Next(0, attackRange.Count() - 1)];
            GameObject obj3 = DataBase.objs[k, l];
            gameManage.requestEnqueue(obj2);
            await Task.Delay(1000);
            gameManage.requestEnqueue(obj3);
        }

    }
    public async static void PassAI()
    {
        await Task.Delay(1000);
        gameManage.Skip();
        await Task.Delay(1000);
        gameManage.Skip();
    }
    public static void StartAI(int mode)
    {
        switch (mode)
        {
            case 0:
                RandomAI();
                break;
            case 1:
                PassAI();
                break;
            default:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
