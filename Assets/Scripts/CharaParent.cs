using System.Collections.Generic;
using UnityEngine;
using static DataBase;

public class CharaParent : MonoBehaviour,interFace
{
    protected string charaName;
    protected int HitPoint;
    private int AttackPower;
    protected NowPoint now = new NowPoint();
    protected List<(int,int)> moveRange = new List<(int,int)>();
    protected List<(int,int)> attackRange = new List<(int, int)>();
    public void Init(int x, int y)
    {
        now.xAxis = x;
        now.yAxis = y;
    }
    public void Erase()
    {
        Destroy(this.gameObject);
    }
    public void AddDamage(int damage)
    {
        HitPoint -= damage;
        if (HitPoint < 0) Erase();
    }

    public string GetName()
    {
        return charaName;
    }
    public List<(int,int)> Movable()
    {
        List<(int,int)> res = new List<(int,int)>();
        foreach((int,int) T in moveRange)
        {
            var (i, j) = T;
            
                if (DataBase.CanSet(now.xAxis + i, now.yAxis + j))
                {
                    res.Add((now.xAxis + i, now.yAxis + j));
                }
            
        }
        return res;
    }
    public List<(int, int)> Attackable()
    {
        List<(int, int)> res = new List<(int, int)>();
        foreach ((int, int) T in attackRange)
        {
            var (i, j) = T;
            if (DataBase.CanSet(now.xAxis+i, now.yAxis+j))
            {
                res.Add((i, j));
            }
        }
        return res;
    }

    private void Start()
    {

    }
}
public class NowPoint
{
    public int xAxis;
    public int yAxis;
}