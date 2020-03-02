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

    private async static void WarikomiAI()
    {
        if (DangerForAI.overDanger)
        {
            ReadAI(true);
            return;
        }
        else if (DangerForAI.danger)
        {
            List<GameObject> res = DataBase.MyKoma(gameManage.turn, true);
            foreach(GameObject obj in res)
            {
                foreach((int,int) T in obj.GetComponent<interFace>().Movable())
                {
                    if (T == DangerForAI.dangerList[0])
                    {
                        var (x, y) = T;
                        GameObject obj1 = DataBase.objs[x, y];
                        await Task.Delay(1000);
                        gameManage.requestEnqueue(obj);
                        await Task.Delay(1000);
                        gameManage.requestEnqueue(obj1);
                        await Task.Delay(100);
                        List<GameObject> res1 = DataBase.MyKoma(gameManage.turn, false);
                        (GameObject, GameObject) Act = (null, null);
                        int ActionPoint = -1000;
                        for (int k = 0; k < res1.Count(); k++)
                        {
                            GameObject obj2 = res1[k];
                            List<(int, int)> attackRange = obj2.GetComponent<interFace>().Attackable();
                            for (int l = 0; l < attackRange.Count(); l++)
                            {
                                var (x3, y3) = attackRange[l];
                                GameObject obj3 = DataBase.objs[x3, y3];
                                int evaAtc;
                                evaAtc = obj3 == DangerForAI.dangerEnemyG ? 10000000 : obj3.GetComponent<interFace>().AtcEvaluation();
                                if (evaAtc > ActionPoint)
                                {
                                    ActionPoint = evaAtc;
                                    Act = (obj2, obj3);
                                }
                            }
                        }
                        Debug.Log("評価値は" + ActionPoint.ToString());
                        var (Robj, Robj1) = Act;
                        await Task.Delay(900);
                        gameManage.requestEnqueue(Robj);
                        await Task.Delay(1000);
                        gameManage.requestEnqueue(Robj1);
                        await Task.Delay(100);
                        DangerForAI.DangerReset();
                        return;
                    }
                }
            }
            ReadAI(true);
            return;
        }
        else
        {
            ReadAI(false);
            DangerForAI.DangerReset();
            return;
        }
    }
    private async static void ReadAI(bool mode)
    {
        List<GameObject> res = DataBase.MyKoma(gameManage.turn, true);
        if (!res.Any())
        {
            aiPlaying = false;
            gameManage.Skip();
            aiPlaying = true;
            await Task.Delay(100);

            if (gameManage.receiveMode != gameManage.situation.attackselect)
            {
                return;
            }
            else
            {
                (GameObject, GameObject) Act = (null, null);
                int ActionPoint = -1000;
                List<GameObject> res1 = DataBase.MyKoma(gameManage.turn, false);
                for (int k = 0; k < res1.Count(); k++)
                {

                    GameObject obj2 = res1[k];
                    List<(int, int)> attackRange = obj2.GetComponent<interFace>().Attackable();
                    for (int l = 0; l < attackRange.Count(); l++)
                    {
                        var (x3, y3) = attackRange[l];
                        GameObject obj3 = DataBase.objs[k, l];
                        int evaAtc = obj3.GetComponent<interFace>().AtcEvaluation();
                        int actPt = evaAtc;
                        if (actPt > ActionPoint)
                        {
                            ActionPoint = actPt;
                            Act = (obj2, obj3);
                        }
                    }
                }
                var (Robj, Robj1) = Act;
                Debug.Log("評価値は" + ActionPoint.ToString());
                gameManage.requestEnqueue(Robj);
                await Task.Delay(1000);
                gameManage.requestEnqueue(Robj1);
                await Task.Delay(100);
            }
        }
        else
        {
            (GameObject, GameObject, GameObject, GameObject) Act = (null, null, null, null);
            int ActionPoint = -1000;
            bool afterSkip = false;
            for (int i = 0; i < res.Count(); i++)
            {
                GameObject obj = res[i];
                var (x0, y0) = DataBase.objSearch(obj);
                List<(int, int)> moveRange = obj.GetComponent<interFace>().Movable();
                for (int j = 0; j < moveRange.Count(); j++)
                {
                    var (x1, y1) = moveRange[j];
                    GameObject obj1 = DataBase.objs[x1, y1];
                    obj.GetComponent<interFace>().Move(x1, y1);
                    DataBase.Set(x0, y0, 0);
                    DataBase.Set(x1, y1, 2);
                    DataBase.objs[x0, y0] = obj1;
                    DataBase.objs[x1, y1] = obj;
                    List<GameObject> res1 = DataBase.MyKoma(gameManage.turn, false);
                    int eva0 = obj.GetComponent<interFace>().Evaluation(x0, y0);
                    int eva1 = obj.GetComponent<interFace>().Evaluation(x1, y1);
                    int movePt = eva1 - eva0;
                    if (!res1.Any())
                    {
                        
                        if (movePt > ActionPoint)
                        {
                            ActionPoint = movePt;
                            Act = (obj, obj1, null, null);
                            afterSkip = true;
                        }
                    }
                    else
                    {
                        for (int k = 0; k < res1.Count(); k++)
                        {
                            GameObject obj2 = res1[k];
                            List<(int, int)> attackRange = obj2.GetComponent<interFace>().Attackable();
                            for (int l = 0; l < attackRange.Count(); l++)
                            {
                                var (x3, y3) = attackRange[l];
                                GameObject obj3 = DataBase.objs[x3, y3];
                                int evaAtc;
                                if (mode)
                                {
                                    evaAtc = obj3 == DangerForAI.dangerEnemyG ? 10000000 : obj3.GetComponent<interFace>().AtcEvaluation();
                                }
                                else
                                {
                                    evaAtc = obj3.GetComponent<interFace>().AtcEvaluation();
                                }
                                int actPt = movePt * 10 + evaAtc;
                                if (actPt > ActionPoint)
                                {
                                    ActionPoint = actPt;
                                    Act = (obj, obj1, obj2, obj3);
                                    afterSkip = false;
                                }
                            }
                        }

                    }
                    DataBase.Set(x0, y0, 2);
                    DataBase.Set(x1, y1, 0);
                    obj.GetComponent<interFace>().Move(x0, y0);
                    DataBase.objs[x0, y0] = obj;
                    DataBase.objs[x1, y1] = obj1;
                }
            }
            var (Robj1, Robj2, Robj3, Robj4) = Act;
            if (afterSkip)
            {
                Debug.Log("評価値は" + ActionPoint.ToString());
                await Task.Delay(1000);
                gameManage.requestEnqueue(Robj1);
                await Task.Delay(1000);
                gameManage.requestEnqueue(Robj2);
                await Task.Delay(100);
                if (gameManage.receiveMode != gameManage.situation.attackselect)
                {
                    return;
                }
                else
                {
                    gameManage.Skip();//何か問題が生じた時に止まらないための処置.正しく動けばこれは起きない
                }
            }
            else
            {
                Debug.Log("評価値は"+ActionPoint.ToString());
                await Task.Delay(1000);
                gameManage.requestEnqueue(Robj1);
                await Task.Delay(1000);
                gameManage.requestEnqueue(Robj2);
                await Task.Delay(1000);
                gameManage.requestEnqueue(Robj3);
                await Task.Delay(1000);
                gameManage.requestEnqueue(Robj4);
                await Task.Delay(100);
                DangerForAI.DangerReset();
            }
        }
        
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
                int eva0 = myobj3.GetComponent<interFace>().AtcEvaluation();
                if (MaxEva < eva0)
                {
                    RandomMax = (myobj2, myobj3);
                    MaxEva = eva0;
                }
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
            case 4:
                ReadAI(false);
                break;
            case 5:
                WarikomiAI();
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
