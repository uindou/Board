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
        this.HitPoint = 1;
        this.AttackPower = 1;
        this.MaxHitPoint = this.HitPoint;
        this.charaName = "Soldier";
        this.makeHP();
    }


    private void initMoveRange()
    {
        this.moveRange.Add((-1, 0));
        this.moveRange.Add((0, -1));
        this.moveRange.Add((0, 1));
        this.moveRange.Add((1, 0));
        this.moveRange.Add((1, 1));
        this.moveRange.Add((1, -1));
        this.moveRange.Add((-1, 1));
        this.moveRange.Add((-1, -1));
        this.moveRange.Add((-2, 0));
        this.moveRange.Add((0, 2));
        this.moveRange.Add((0, -2));

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
