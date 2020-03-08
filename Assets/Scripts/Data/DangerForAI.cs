﻿
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
    public static (int, int) dangerEnemy;
    public static bool isNextDead;
    public static GameObject dangerEnemyG;
    private void Start()
    {
        DangerReset();
    }
    public static void AddDanger((int,int) T,(int,int)S,GameObject G)
    {
        danger = true;
        dangerEnemy = S;
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
    }

}
