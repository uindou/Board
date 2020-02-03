using System.Collections;
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
        makeHP();
        this.HitPoint = 3;
        this.MaxHitPoint = this.HitPoint;
        this.charaName = "Tank";
    }

    private void makeHP()
    {
        HitPoint = 3; //なぜか0になってるのでここでもう一回代入
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
