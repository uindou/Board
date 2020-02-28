using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine.SceneManagement;

public class Soldier : CharaParent
{
    private List<(int, int)> MArange;
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
        if (SceneManager.GetActiveScene().name == "AIStage1" || SceneManager.GetActiveScene().name == "AIStage2") initAIRange();
    }
    public async override void AttackImage()
    {
        for(int i = 0; i < 2; i++)
        {
            this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = DataBase.image(8);
            await Task.Delay(200);
            this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = DataBase.image(11);
            await Task.Delay(200);
        }
        this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = DataBase.image(0);
    }
    public async override void DamageImage()
    {
        this.gameObject.transform.GetChild(0).GetComponent<Image>().color = Color.red;
        await Task.Delay(200);
        this.gameObject.transform.GetChild(0).GetComponent<Image>().color = Color.white;
    }
    public override int AtcEvaluation()
    {
        return 10* Evaluation(DataBase.vertical - now.xAxis, now.yAxis);
    }
    /*public override int Evaluation(int x,int y)
    {
        if (DangerForAI.danger)
        {
            if (DangerForAI.overDanger)
            {
                foreach((int,int) T in MArange)
                {
                    if(T == )
                }
            }

        }
        else
        {
            return -1;
        }
    }*/
    public override int Evaluation(int x, int y)
    {
        return -1;
    }

    /*public override List<(int, int)> Movable()
    {
        List<(int, int)> res = new List<(int, int)>();
        foreach ((int, int) T in moveRange)
        {
            var (i, j) = T;
            if (gameManage.turn == false)
            {
                if (DataBase.CanSet(now.xAxis + i, now.yAxis + j) && DataBase.Jump(now.xAxis,now.yAxis,now.xAxis + i, now.yAxis + j))
                {
                    res.Add((now.xAxis + i, now.yAxis + j));
                }
            }
            else
            {
                if (DataBase.CanSet(now.xAxis - i, now.yAxis - j))
                {
                    res.Add((now.xAxis - i, now.yAxis - j));
                }
            }
        }
        return res;
    }*/
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
        this.moveRange.Add((2, 0));

    }
    private void initAIRange()
    {
        this.AIRange.Add((-1, 0));
        this.AIRange.Add((0, -1));
        this.AIRange.Add((0, 1));
        this.AIRange.Add((-1, 1));
        this.AIRange.Add((-1, -1));
        this.AIRange.Add((-2, 0));
        this.AIRange.Add((-1, 1));
        this.AIRange.Add((-1, -1));
        this.AIRange.Add((-2, 0));
        this.AIRange.Add((-2, 0));
        this.AIRange.Add((0, 2));
        this.AIRange.Add((0, -2));
    }
    private void initAttackRange()
    {
        this.attackRange.Add((-1, 0));
        this.attackRange.Add((0, -1));
        this.attackRange.Add((0, 1));
        this.attackRange.Add((1, 0));
    }
    private void initMArange()
    {
        foreach((int,int) T in moveRange)
        {
            var (x1, y1) = T;
            foreach ((int,int) S in attackRange)
            {

                var (x2, y2) = S;
                MArange.Add((x1 + x2, y1 + y2));
            }
        }
        MArange = MArange.Distinct().ToList();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
