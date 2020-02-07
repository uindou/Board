﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataBase;

public class CharaParent : MonoBehaviour,interFace
{
    protected string charaName;
    protected int HitPoint;
    protected int MaxHitPoint;
    protected int AttackPower;
    protected bool team;
    protected NowPoint now = new NowPoint();
    protected List<(int,int)> moveRange = new List<(int,int)>();
    protected List<(int,int)> attackRange = new List<(int, int)>();
    public void Init(int x, int y,bool team)
    {
        now.xAxis = x;
        now.yAxis = y;
        this.team = team;
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

        //Destroy(this.gameObject);
    }
    public void AddDamage(int damage)
    {
        HitPoint -= damage;
        if (HitPoint <= 0) Erase();
    }
    public bool IsTeam()
    {
        return team;
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
    public List<(int, int)> Attackable()
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

    private void Start()
    {

    }
}
public class NowPoint
{
    public int xAxis;
    public int yAxis;
}