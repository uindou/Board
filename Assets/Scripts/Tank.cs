﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tank : CharaParent
{

    // Start is called before the first frame update
    void Start()
    {
        initMoveRange();
        initAttackRange();
        this.HitPoint = 3;
        this.AttackPower = 1;
        this.MaxHitPoint = HitPoint;
        this.charaName = "Tank";
        makeHP();
    }

    private void makeHP()
    {
        
        for (int i = 0; i < 3; i++)
        {
            if (i < HitPoint)
            {
                this.transform.GetChild(1).transform.GetChild(i).GetComponent<Image>().sprite = DataBase.image(2);
                Debug.Log("ハート");
            }
            else if (i < MaxHitPoint)
            {
                this.transform.GetChild(1).transform.GetChild(i).GetComponent<Image>().sprite = DataBase.image(3);
                Debug.Log("黒ハート");
            }
            else
            {
                this.transform.GetChild(1).transform.GetChild(i).gameObject.SetActive(false);
                Debug.Log("ハートなし");
            }
            Debug.Log(HitPoint);
        }
    }

    private void initMoveRange()
    {
        this.moveRange.Add((-1, 0));
        this.moveRange.Add((0, -1));
        this.moveRange.Add((0, 1));
        this.moveRange.Add((1, 0));
        this.moveRange.Add((-1, -1));
        this.moveRange.Add((-1, 1));

    }
    private void initAttackRange()
    {
        this.attackRange.Add((-1, 0));
        this.attackRange.Add((-2, 0));
        this.attackRange.Add((-2, -1));
        this.attackRange.Add((-2, 1));
    }
    // Update is called once per frame
    void Update()
    {

    }
}
