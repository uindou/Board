using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soldier : CharaParent
{
    // Start is called before the first frame update
    void Start()
    {
        initMoveRange();
        initAttackRange();
        //makeHP();
        this.HitPoint = 1;
        this.AttackPower = 3;
        this.MaxHitPoint = this.HitPoint;
        this.charaName = "Soldier";
    }
    private void makeHP()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < HitPoint)
            {
                this.transform.GetChild(1).transform.GetChild(i).GetComponent<Image>().sprite = DataBase.image(2);
            }
            else if (i < MaxHitPoint)
            {
                this.transform.GetChild(1).transform.GetChild(i).GetComponent<Image>().sprite = DataBase.image(3);
            }
            else
            {
                this.transform.GetChild(1).transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    private void initMoveRange()
    {
        this.moveRange.Add((-1, 0));
        this.moveRange.Add((0, -1));
        this.moveRange.Add((0, 1));
        this.moveRange.Add((1, 0));

    }
    private void initAttackRange()
    {
        this.attackRange.Add((-1, 0));
        this.attackRange.Add((0, -1));
        this.attackRange.Add((0, 1));
        this.attackRange.Add((1, 0));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
