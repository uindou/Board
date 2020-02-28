using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static gameManage;
using static DataBase;

public class clickReceiver : MonoBehaviour
{
    //private float duration = 1.0f;
    private List<(int, int)> select;
    private static bool isFlash;
    private static bool activate;
    //private static Color startcolor;

    private void Start()
    {
        //startcolor = this.GetComponent<Image>().color;
        select = new List<(int, int)>();
        activate = true;
    }
    public void ChangeAct()
    {
        activate = !activate;
    }
    public void OnClick()
    {
        if(activate)gameManage.requestEnqueue(this.gameObject);
    }
    public bool IsRange()
    {
        foreach ((int, int)T in select)
        {
            if (DataBase.objSearch(this.gameObject) == T)
            {
                return true;
            }
        }
        return false;
    }


    public void StopFlash()
    {
        isFlash = false;
    }
   public void Flash()
    {
        isFlash = true;
        select.Add(DataBase.objSearch(this.gameObject));
        StartCoroutine("testtimer");
    }
    IEnumerator testtimer()
    {
        if (gameManage.receiveMode == gameManage.situation.select || gameManage.receiveMode == gameManage.situation.move)
        {
            this.transform.GetChild(2).GetComponent<Image>().sprite = DataBase.image(4);
        }
        else
        {
            this.transform.GetChild(2).GetComponent<Image>().sprite = DataBase.image(6);
        }
        while (isFlash)
        {
            // 残り時間が0以上の場合はタイマーを更新　
            yield return new WaitForSeconds(0.01f);
        }
        this.transform.GetChild(2).GetComponent<Image>().sprite = DataBase.image(7);
        select.Remove(DataBase.objSearch(this.gameObject));
    }
}
