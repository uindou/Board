using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using static gameManage;
using System.Linq;

public class myAI : MonoBehaviour
{

    private async static void RandomAI()
    {
        List<GameObject> res = DataBase.MyKoma(gameManage.turn);
        System.Random r1 = new System.Random();
        GameObject obj = res[r1.Next(0, res.Count() - 1)];
        gameManage.requestEnqueue(obj);

        await Task.Delay(500);

        List<(int,int)> moveRange = obj.GetComponent<interFace>().Movable();
        var (i, j) = moveRange[r1.Next(0, moveRange.Count() - 1)];
        GameObject obj1 = DataBase.objs[i,j];


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
