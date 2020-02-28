using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using static gameManage;
using System.Linq;

public class myAI : MonoBehaviour
{
    private enum KomaNum
    {
        empty,
        soldier,
        tank
    }
    private (((int,int),(int,int)), ((int, int), (int, int))) MiniMaxMain(int readTurn,int[,] board)
    {
        return (((0, 0), (0, 0)), ((0, 0), (0, 0)));
    }
    private async static void PowerfulRandomAI()
    {
        int power1 = 200;
        int power2 = 20;
        List<GameObject> res = DataBase.MyKoma(gameManage.turn, true);//自分の駒のリスト
        if (res == null)
        {
            aiPlaying = false;
            gameManage.Skip();
            aiPlaying = true;
            await Task.Delay(100);
        }
        else
        {
            (GameObject,GameObject) RandomMax = (null,null);
            int MaxEva = -1000;
            for(int t = 0; t < power1; t++)
            {
                GameObject myobj = res[Random.Range(0, res.Count() - 1)];
                List<(int, int)> moveRange = myobj.GetComponent<interFace>().Movable();
                var (i0, j0) = DataBase.objSearch(myobj);
                var (i, j) = moveRange[Random.Range(0, moveRange.Count() - 1)];
                GameObject myobj1 = DataBase.objs[i, j];
                int eva0 = myobj.GetComponent<interFace>().Evaluation(i0, j0);
                int eva1 = myobj.GetComponent<interFace>().Evaluation(i, j);
                if (MaxEva < eva1-eva0)
                {
                    RandomMax = ( myobj,myobj1);
                    MaxEva = eva1-eva0;
                }
            }
            var (obj,obj1) = RandomMax;
            Debug.Log("評価値は"+MaxEva.ToString());


            await Task.Delay(1000);
            gameManage.requestEnqueue(obj);

            await Task.Delay(1000);
            gameManage.requestEnqueue(obj1);
            await Task.Delay(100);
        }
        /*------------------------------------------MOVE PHASE------------------------------------------------------*/
        if (gameManage.receiveMode != gameManage.situation.attackselect)
        {
            return;
        }
        else
        {
            (GameObject, GameObject) RandomMax = (null, null);
            int MaxEva = -1000;
            for (int t = 0; t < power2; t++)
            {
                List<GameObject> res1 = DataBase.MyKoma(gameManage.turn, false);
                GameObject myobj2 = res1[Random.Range(0, res1.Count() - 1)];
                List<(int, int)> attackRange = myobj2.GetComponent<interFace>().Attackable();
                var (k, l) = attackRange[Random.Range(0, attackRange.Count() - 1)];
                GameObject myobj3 = DataBase.objs[k, l];
                RandomMax = (myobj2, myobj3);
            }
            var(obj2, obj3) = RandomMax;
            gameManage.requestEnqueue(obj2);
            await Task.Delay(1000);
            gameManage.requestEnqueue(obj3);

        }
    }
    private async static void FixedRandomAI()
    {
        List<GameObject> res = DataBase.FixedMyKoma(gameManage.turn, true);
        if (res == null)
        {
            aiPlaying = false;
            gameManage.Skip();
            aiPlaying = true;
            await Task.Delay(100);
        }
        else
        {
            System.Random r1 = new System.Random();
            GameObject obj = res[r1.Next(0, res.Count() - 1)];
            List<(int, int)> AIRange = obj.GetComponent<interFace>().AIMovable();
            var (i, j) = AIRange[r1.Next(0, AIRange.Count() - 1)];
            GameObject obj1 = DataBase.objs[i, j];

            await Task.Delay(1000);
            gameManage.requestEnqueue(obj);

            await Task.Delay(1000);
            gameManage.requestEnqueue(obj1);
            await Task.Delay(100);
        }
        if (gameManage.receiveMode != gameManage.situation.attackselect)
        {
            return;
        }
        else
        {
            Debug.Log(3);
            System.Random r1 = new System.Random();
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
            case 2:
                FixedRandomAI();
                break;
            case 3:
                PowerfulRandomAI();
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
