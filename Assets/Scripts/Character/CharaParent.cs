using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using static DataBase;

public class CharaParent : MonoBehaviour,interFace
{
    protected string charaName;
    protected int HitPoint;
    protected int MaxHitPoint;
    protected int AttackPower;
    protected bool team;
    protected NowPoint now = new NowPoint();
    protected List<(int, int)> AIRange = new List<(int, int)>();
    protected List<(int,int)> moveRange = new List<(int,int)>();
    protected List<(int,int)> attackRange = new List<(int, int)>();
    public void Init(int x, int y,bool team)
    {
        now.xAxis = x;
        now.yAxis = y;
        this.team = team;
    }
    public int ShowHP()
    {
        return HitPoint;
    }
    public void Move(int i,int j)
    {
        now.xAxis = i;
        now.yAxis = j;
    }
    public int Power()
    {
        return AttackPower;
    }
    
    public void Erase()
    {
        DataBase.Set(now.xAxis,now.yAxis,0);
        if(team)this.gameObject.transform.Rotate(0, 0, 180f);
        this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = DataBase.firstImage;
        Destroy(this);
    }

    public void makeHP()
    {
        if (this.HitPoint <= 0)
        {
            for (int i = 0; i < 3; i++)
            {
                this.transform.GetChild(1).transform.GetChild(i).GetComponent<Image>().sprite = DataBase.image(5);
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                if (i < this.HitPoint)
                {
                    this.transform.GetChild(1).transform.GetChild(i).GetComponent<Image>().sprite = DataBase.image(2);
                }
                else if (i < this.MaxHitPoint)
                {
                    this.transform.GetChild(1).transform.GetChild(i).GetComponent<Image>().sprite = DataBase.image(3);
                }
                else
                {
                    this.transform.GetChild(1).transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }
    public void AddDamage(int damage)
    {
        HitPoint -= damage;
        makeHP();
        if (HitPoint <= 0)
        {
            Erase();
        }
        else
        {
            this.DamageImage();
        }
    }
    public bool IsTeam()
    {
        return team;
    }

    public string GetName()
    {
        return charaName;
    }
    public List<(int,int)> AIMovable()
    {
        List<(int, int)> res = new List<(int, int)>();
        foreach ((int, int) T in AIRange)
        {
            var (i, j) = T;
            if (gameManage.turn == false)
            {
                if (DataBase.CanSet(now.xAxis + i, now.yAxis + j))
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
    }
    /*------------------------------------------------OVERRIDE----------------------------------------------------------------*/
    public async virtual void AttackImage()
    {

    }
    public async  void DamageImage()
    {
        for (int i = 0; i < 2; i++)
        {
            this.gameObject.transform.GetChild(0).GetComponent<Image>().color = Color.red;
            await Task.Delay(100);
            this.gameObject.transform.GetChild(0).GetComponent<Image>().color = Color.white;
            await Task.Delay(100);

        }
    }
    public virtual List<(int,int)> Movable()
    {
        List<(int,int)> res = new List<(int,int)>();
        foreach((int,int) T in moveRange)
        {
            var (i, j) = T;
            if (gameManage.turn == false)
            {
                if (DataBase.CanSet(now.xAxis + i, now.yAxis + j))
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
    }
    public virtual List<(int, int)> Attackable()
    {
        List<(int, int)> res = new List<(int, int)>();
        foreach ((int, int) T in attackRange)
        {
            var (i, j) = T;
            if (gameManage.turn == false)
            {
                if (DataBase.CanAttack(now.xAxis + i, now.yAxis + j, !gameManage.turn))
                {
                    res.Add((now.xAxis + i, now.yAxis + j));
                }
            }
            else
            {
                if (DataBase.CanAttack(now.xAxis - i, now.yAxis - j, !gameManage.turn))
                {
                    res.Add((now.xAxis - i, now.yAxis - j));
                }
            }
        }
        return res;
    }

    public virtual int Evaluation(int x,int y)
    {
        return 0;
    }
    public virtual int AtcEvaluation()
    {
        return 0;
    }
/*------------------------------------------------OVERRIDE----------------------------------------------------------------*/
}
public class NowPoint
{
    public int xAxis;
    public int yAxis;
}