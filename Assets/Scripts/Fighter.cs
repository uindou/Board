using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class Fighter : CharaParent
{
    void Start()
    {
        initMoveRange();
        initAttackRange();
        this.HitPoint = 2;
        this.AttackPower = 1;
        this.MaxHitPoint = this.HitPoint;
        this.charaName = "PlainFighter";
        this.makeHP();
        if (SceneManager.GetActiveScene().name == "AIStage1") initAIRange();
    }
    public async override void AttackImage()
    {
        for (int i = 0; i < 2; i++)
        {
            this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = DataBase.image(10);
            await Task.Delay(200);
            this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = DataBase.image(13);
            await Task.Delay(200);
        }
        this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = DataBase.image(14);
    }
    public async override void DamageImage()
    {
        this.gameObject.transform.GetChild(0).GetComponent<Image>().color = Color.red;
        await Task.Delay(200);
        this.gameObject.transform.GetChild(0).GetComponent<Image>().color = Color.white;
    }

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
        this.attackRange.Add((0, -1));
        this.attackRange.Add((0, 1));
        this.attackRange.Add((1, 0));
    }
    // Update is called once per frame
    void Update()
    {

    }
}
