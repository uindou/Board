using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataBase;

public class Init : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<(int, int,bool, string)> stage = DataBase.makeStage();
        foreach((int, int,bool, string) T in stage)
        {
            var (i, j,team, name) = T;
            int teamColor = team?2:1;
            i -= 1;
            j -= 1;
            DataBase.Set(i, j, teamColor);

            GameObject obj = objs[i, j];
            switch(name)
            {
                case "Soldier":
                    obj.AddComponent<Soldier>();
                    obj.GetComponent<interFace>().Init(i, j,team);
                    obj.transform.GetChild(0).GetComponent<Image>().sprite = DataBase.image(0);
                    if (team)
                    {
                        obj.transform.Rotate(0, 0, 180f);
                    }
                    break;

                case "Tank":
                    obj.AddComponent<Tank>();
                    obj.GetComponent<interFace>().Init(i, j, team);
                    obj.transform.GetChild(0).GetComponent<Image>().sprite = DataBase.image(1);
                    if (team)
                    {
                        obj.transform.Rotate(0, 0, 180f);
                    }
                    break;
                case "PlainFighter":
                    obj.AddComponent<Fighter>();
                    obj.GetComponent<interFace>().Init(i, j, team);
                    obj.transform.GetChild(0).GetComponent<Image>().sprite = DataBase.image(14);
                    if (team)
                    {
                        obj.transform.Rotate(0, 0, 180f);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
