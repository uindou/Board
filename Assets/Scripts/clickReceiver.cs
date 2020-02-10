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
    //private static Color startcolor;

    private void Start()
    {
        //startcolor = this.GetComponent<Image>().color;
        select = new List<(int, int)>();
    }
    public void OnClick()
    {
        Debug.Log("Clicked",this);
        gameManage.requestEnqueue(this.gameObject);
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
        Debug.Log("change",this);
        isFlash = true;
        select.Add(DataBase.objSearch(this.gameObject));
        StartCoroutine("testtimer", 5);
    }
    IEnumerator testtimer(int lefttime)
    {
        while (lefttime >= 0)
        {
            if (!isFlash) break;
            // 残り時間が0以上の場合はタイマーを更新　
            yield return new WaitForSeconds(0.01f);
            /*float phi = Time.time / duration * 2 * Mathf.PI;
            float amplitude = Mathf.Cos(phi) * 0.5F + 0.5F;
            this.GetComponent<Image>().color = new Color(amplitude, 100, amplitude);*/
            this.transform.GetChild(2).GetComponent<Image>().sprite = DataBase.image(4);
        }
        //this.GetComponent<Image>().color = startcolor;
        this.transform.GetChild(2).GetComponent<Image>().sprite = DataBase.image(5);
        select.Remove(DataBase.objSearch(this.gameObject));
    }
}
