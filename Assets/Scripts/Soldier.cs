using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : CharaParent
{

    private int i;
    // Start is called before the first frame update
    void Start()
    {
        initMoveRange();
        initAttackRange();
        makeHP();
        this.HitPoint = 1;
        this.MaxHitPoint = 1;
        this.charaName = "Soldier";
    }
    private void makeHP()
    {
        for (i = 0; i < 3; i++)
        {
            if (i < HitPoint)
            {
                this.transform.GetChild(i).GetComponent<Image>().sprite = heart;
            }DataBase.images[1]
            else if (i < MaxHitPoint)
            {
                this.transform.GetChild(i).GetComponent<Image>().sprite = brokenheart;
            }
            else
            {
                this.transform.GetChild(i).gameObject.SetActive(false);
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
