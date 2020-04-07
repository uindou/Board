
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DangerForAI : MonoBehaviour
{
    public static bool danger;
    public static int HP;
    public static bool overDanger;
    public static List<(int, int)> dangerList;
    public static bool[] dangers;

    public static (int, int) dangerEnemyPlace;
    public static bool isNextDead;
    public static GameObject dangerEnemyG;
    private void Start()
    {
        dangers = new bool[DataBase.horizontal];
        DangerReset();
    }
    public static void AddStopper(int y)
    {
        dangers[y] = true;
    }
    public static void AddDanger((int,int)T,GameObject G)
    {
        danger = true;
        
        dangerEnemyG = G;

        dangerList.Add(T);
        HP = G.GetComponent<interFace>().ShowHP();
        
        if (dangerList.Count() > 1) overDanger = true;
    }
    public static void DangerReset()
    {
        isNextDead = false;
        danger = false;
        overDanger = false;
        dangerList = new List<(int, int)>();
        for(int i = 0; i < DataBase.horizontal; i++)
        {
            dangers[i] = false;
        }
    }

}
