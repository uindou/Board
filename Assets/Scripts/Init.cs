using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataBase;

public class Init : MonoBehaviour
{
    public Sprite[] images = new Sprite[10];
    // Start is called before the first frame update
    void Start()
    {
        List<(int, int,bool, string)> stage = DataBase.makeStage();
        Debug.Log("init　起動成功");
        foreach((int, int,bool, string) T in stage)
        {
            var (i, j,team, name) = T;
            int teamColor = team?1:0;
            DataBase.Set(i, j, teamColor);

            GameObject obj = GameObject.Find("Grid(" + i + "," + j+")");
            switch(name)
            {
                case "Soldier":
                    obj.AddComponent<Soldier>();
                    obj.GetComponent<interFace>().Init(i, j);
                    obj.transform.GetChild(0).GetComponent<Image>().sprite = images[0];
                    if (team)
                    {
                        //obj.transform.Rotate(0, 180f, 0);
                    }
                    break;
                default:
                    obj.AddComponent<swap>();
                    break;
            }
        }
    }
}
