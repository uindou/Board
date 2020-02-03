﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : CharaParent
{

    // Start is called before the first frame update
    void Start()
    {
        initMoveRange();
        initAttackRange();
        this.HitPoint = 3;
        this.charaName = "Tank";
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