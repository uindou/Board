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

            i -= 1;
            j -= 1;
            if (!DataBase.isRev)
            {

            }
            else
            {
                team = !team;
                i = (DataBase.vertical - 1) - i;
                j = (DataBase.horizontal - 1) - j;
            }
            int teamColor = team ? 2 : 1;
            DataBase.Set(i, j, teamColor);

            GameObject obj = objs[i, j];
            switch(name)
            {
                case "Soldier":
                    obj.AddComponent<Soldier>();
                    obj.GetComponent<interFace>().Init(i, j,team);
                    
                    if (team)
                    {
                        obj.transform.GetChild(0).GetComponent<Image>().sprite = DataBase.image((int)DataBase.im.enemySoldier);
                        obj.transform.Rotate(0, 0, 180f);
                    }
                    else
                    {
                        obj.transform.GetChild(0).GetComponent<Image>().sprite = DataBase.image(0);
                    }
                    break;

                case "Tank":
                    obj.AddComponent<Tank>();
                    obj.GetComponent<interFace>().Init(i, j, team);
                    
                    if (team)
                    {
                        obj.transform.Rotate(0, 0, 180f);
                        obj.transform.GetChild(0).GetComponent<Image>().sprite = DataBase.image((int)DataBase.im.enemyTank);
                    }
                    else
                    {
                        obj.transform.GetChild(0).GetComponent<Image>().sprite = DataBase.image(1);
                    }
                    break;
                case "PlainFighter":
                    obj.AddComponent<Fighter>();
                    obj.GetComponent<interFace>().Init(i, j, team);
                    
                    if (team)
                    {
                        obj.transform.Rotate(0, 0, 180f);
                        obj.transform.GetChild(0).GetComponent<Image>().sprite = DataBase.image((int)DataBase.im.enemyFighter);
                    }
                    else
                    {
                        obj.transform.GetChild(0).GetComponent<Image>().sprite = DataBase.image(14);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
