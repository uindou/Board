using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkinTabSetting : MonoBehaviour
{
    public int mode;
    public (GameObject,int) Soldier;
    public (GameObject,int) Tank;
    public (GameObject,int) Fighter;
    public GameObject Tabs;
    private (GameObject, int)[] TabList;
    private void Awake()
    {
        Soldier = (Tabs.transform.GetChild(2).GetChild(0).gameObject, 0);
        Tank = (Tabs.transform.GetChild(1).gameObject, 1);
        Fighter = (Tabs.transform.GetChild(0).gameObject, 2);
        TabList = new (GameObject, int)[] { Soldier, Tank, Fighter };
        TabInit();
    }
    private void TabInit()
    {
        var (g1, _) = Soldier;
        var (g2, _) = Tank;
        var (g3, _) = Fighter;
        g1.GetComponent<Image>().color = Color.white;
        g2.GetComponent<Image>().color = Color.gray;
        g3.GetComponent<Image>().color = Color.gray;
    }
    public void OnEnable()
    {
        TabInit();
    }
    public void OnClick()
    {
        SkinViewManager.ModeChange(mode);
        foreach(var(obj,num) in TabList)
        {
            if (this.mode == num)
            {
                obj.GetComponent<Image>().color = Color.white;
            }
            else
            {
                obj.GetComponent<Image>().color = Color.gray;
            }
        }
    }
}
