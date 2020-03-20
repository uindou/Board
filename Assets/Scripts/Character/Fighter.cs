using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class Fighter : CharaParent
{
    private int inf = 10^10;
    private int[,] eva;
    void Start()
    {
        eva = new int[9, 7] { {1,1,1,1,1,1,1 },{ 3,3,3,3,3,3,3},{5,5,5,5,5,5,5 },{10,10,10,10,10,10,10 },{25,25,25,25,25,25,25 },
            {50,50,50,50,50,50,50 },{50,50,100,100,100,50,50 },{ 1, 1, 1, 1, 1, 1, 1 },{ 10000, 10000, 10000, 10000, 10000, 10000, 10000 }};
        initMoveRange();
        initAttackRange();
        this.HitPoint = 2;
        this.AttackPower = 1;
        this.MaxHitPoint = this.HitPoint;
        this.charaName = "PlainFighter";
        this.makeHP();
        if (SceneManager.GetActiveScene().name == "AIStage1" || SceneManager.GetActiveScene().name == "AIStage2") initAIRange();
    }
    /*--------------------------------------------------OVERRIDE ZONE-----------------------------------------------------*/
    public override void AttackSound()
    {
        SoundPlay.FighterPlay();
    }
    public async override void AttackImage()
    {
        for (int i = 0; i < 2; i++)
        {
            this.gameObject.transform.GetChild(3).GetComponent<Image>().sprite = DataBase.image((int)DataBase.im.plainA1);
            await Task.Delay(200);
            this.gameObject.transform.GetChild(3).GetComponent<Image>().sprite = DataBase.image((int)DataBase.im.plainA2);
            await Task.Delay(200);
        }
        this.gameObject.transform.GetChild(3).GetComponent<Image>().sprite = DataBase.image((int)DataBase.im.transParent);
    }
  

    public override int Evaluation(int x, int y)
    {
        return eva[x,y];
    }
    public override int AtcEvaluation()
    {
        int rev = this.HitPoint == 2 ? 2 : 1;
        return 3 * Evaluation(DataBase.vertical - now.xAxis, now.yAxis) * rev;
    }
    /*--------------------------------------------OVERRIDE END----------------------------------------------------*/
    private void initMoveRange()
    {
        this.moveRange.Add((-3, 0));
        this.moveRange.Add((-2, 2));
        this.moveRange.Add((-2, -2));
        this.moveRange.Add((2, 0));
        this.moveRange.Add((1, 1));
        this.moveRange.Add((1, -1));

    }
    private void initAIRange()
    {
        this.AIRange.Add((-3, 0));
        this.AIRange.Add((-2, 2));
        this.AIRange.Add((-2, -2));
        this.AIRange.Add((-3, 0));
        this.AIRange.Add((-2, 2));
        this.AIRange.Add((-2, -2));
        this.AIRange.Add((-3, 0));
        this.AIRange.Add((-2, 2));
        this.AIRange.Add((-2, -2));
        this.AIRange.Add((2, 0));
        this.AIRange.Add((1, 1));
    }
    private void initAttackRange()
    {
        this.attackRange.Add((-1, 0));
        this.attackRange.Add((-2, 0));
    }


}
