using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataBase;

public class Init : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<(int, int,bool, string)> stage = DataBase.stage;
        foreach((int, int,bool, string) T in stage)
        {
            var (i, j,team, name) = T;
            int teamColor = 1 if team else 0;
            DataBase.Set(i, j, teamColor);
        }
    }
}
