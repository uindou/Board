using System.Collections.Generic;
using UnityEngine;
using static DataBase;

public class CharaParent : MonoBehaviour,interFace
{
    private string charaName;
    private int HitPoint;
    private int AttackPower;
    NowPoint now = new NowPoint();
    private List<(int,int)> moveRange = new List<(int,int)>();
    private List<(int,int)> attackRange = new List<(int, int)>();
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
            if (DataBase.CanSet(i, j))
            {
                res.Add((i, j));
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