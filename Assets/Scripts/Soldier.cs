using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : CharaParent
{

    // Start is called before the first frame update
    void Start()
    {
        initMoveRange();
        initAttackRange();
        this.HitPoint = 3;
        this.charaName = "Soldier";
    }

    private void initMoveRange()
    {
        this.moveRange.Add((0, 1));
        this.moveRange.Add((0, 2));
        this.moveRange.Add((-1, 1));
        this.moveRange.Add((-1, -1));
        this.moveRange.Add((-1, 0));
        this.moveRange.Add((-2, 0));
        this.moveRange.Add((0, -1));
        this.moveRange.Add((0, -2));
    }
    private void initAttackRange()
    {
        this.attackRange.Add((0, 1));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
