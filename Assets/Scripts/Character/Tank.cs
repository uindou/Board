using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class Tank : CharaParent
{
    private int inf = 10 ^ 10;
    private int[,] eva;
    // Start is called before the first frame update
    void Start()
    {
        eva = new int[9, 7] { {1,1,1,1,1,1,1 },{10,30,20,30,20,30,10},{10,31,21,31,21,31,10},{10,32,22,32,22,32,10},
            {10,33,23,33,23,33,10},{10,34,24,34,24,34,10},{10,35,25,35,25,35,10},{10,36,26,36,26,36,10},{ inf, inf, inf, inf, inf, inf, inf }};
        initMoveRange();
        initAttackRange();
        this.HitPoint = 3;
        this.AttackPower = 1;
        this.MaxHitPoint = HitPoint;
        this.charaName = "Tank";
        this.makeHP();
        if (SceneManager.GetActiveScene().name == "AIStage1" || SceneManager.GetActiveScene().name == "AIStage2") initAIRange();
    }
    /*--------------------------------------------------OVERRIDE ZONE-----------------------------------------------------*/
    public async override void AttackImage()
    {
        for (int i = 0; i < 2; i++)
        {
            this.gameObject.transform.GetChild(3).GetComponent<Image>().sprite = DataBase.image(9);
            await Task.Delay(200);
            this.gameObject.transform.GetChild(3).GetComponent<Image>().sprite = DataBase.image(12);
            await Task.Delay(200);
        }
        this.gameObject.transform.GetChild(3).GetComponent<Image>().sprite = DataBase.image((int)DataBase.im.transParent);
    }
    public async override void DamageImage()
    {
        this.gameObject.transform.GetChild(0).GetComponent<Image>().color = Color.red;
        await Task.Delay(200);
        this.gameObject.transform.GetChild(0).GetComponent<Image>().color = Color.white;
    }
    public override int Evaluation(int x, int y)
    {
        return eva[x, y];
    }
    public override int AtcEvaluation()
    {
        int rev = this.HitPoint == 3 ? 1 : (this.HitPoint ==2? 2:10);
        return 2 * Evaluation(DataBase.vertical-now.xAxis, now.yAxis) * rev;
    }
    /*--------------------------------------------------OVERRIDE END--------------------------------------------------*/
    /*--------------------------------------------------INIT ZONE-----------------------------------------------------*/
    private void initMoveRange()
    {
        this.moveRange.Add((-1, 0));
        this.moveRange.Add((0, -1));
        this.moveRange.Add((0, 1));
        this.moveRange.Add((1, 0));
    }
    private void initAIRange()
    {
        this.AIRange.Add((-1, 0));
        this.AIRange.Add((-1, 0));
        this.AIRange.Add((-1, 0));
        this.AIRange.Add((0, -1));
        this.AIRange.Add((0, 1));
    }
    private void initAttackRange()
    {
        this.attackRange.Add((-1, 0));
        this.attackRange.Add((-2, 0));
        this.attackRange.Add((-2, -1));
        this.attackRange.Add((-2, 1));
    }
    /*--------------------------------------------------INIT END-----------------------------------------------------*/
    
}
