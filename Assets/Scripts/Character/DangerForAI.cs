using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerForAI : MonoBehaviour
{
    public static bool danger;
    public static bool overDanger;
    public static List<(int, int)> dangerList;
    public static (int, int) dangerEnemy;
    private void Start()
    {
        danger = false;
        overDanger = false;
    }

}
