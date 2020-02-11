﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class Tank : CharaParent
{

    // Start is called before the first frame update
    void Start()
    {
        initMoveRange();
        initAttackRange();
        this.HitPoint = 3;
        this.AttackPower = 1;
        this.MaxHitPoint = HitPoint;
        this.charaName = "Tank";
        this.makeHP();
        if (SceneManager.GetActiveScene().name == "AIStage1") initAIRange();
    }
    public async override void AttackImage()
    {
        for (int i = 0; i < 2; i++)
        {
            this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = DataBase.image(9);
            await Task.Delay(200);
            this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = DataBase.image(12);
            await Task.Delay(200);
        }
        this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = DataBase.image(1);
    }
    public async override void DamageImage()
    {
        this.gameObject.transform.GetChild(0).GetComponent<Image>().color = Color.red;
        await Task.Delay(200);
        this.gameObject.transform.GetChild(0).GetComponent<Image>().color = Color.white;
    }
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
    // Update is called once per frame
    void Update()
    {

    }
}
