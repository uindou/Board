using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinViewManager : MonoBehaviour
{
    private Sprite currentSkinImage;
    private string currentSkinName;
    public GameObject currentSkin;
    public GameObject skinMatrix;
    public static int page;
    public string charaName;

    // Start is called before the first frame update
    void Start()
    {
        page = 1;
        SkinView();
    }

    private void OnEnable()
    {
        page = 1;
        SkinView();
        Debug.Log("onenable");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Equip()
    {

        //装備中のスキン画像と名前を表示
        currentSkin.transform.GetChild(0).GetComponent<Image>().sprite = currentSkinImage;
        currentSkin.transform.GetChild(1).GetComponent<Text>().text = currentSkinName;
    }

    public void SkinView()
    {
        //表示する6要素をリストから選択
        List<(string, Sprite, int, bool)> skinInfo = SkinManager.SoldierSkin.GetRange((page-1)*6,6);

        //行列に各々表示
        for(int i=0;i<6; i++)
        {
            var (name, image, price, flug) = skinInfo[i];

            
            skinMatrix.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = image;
            skinMatrix.transform.GetChild(i).transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = price.ToString();

            //買ってたら"Parchased"を表示
            if (flug)
            {
                skinMatrix.transform.GetChild(i).transform.GetChild(1).gameObject.SetActive(false);
                skinMatrix.transform.GetChild(i).transform.GetChild(2).gameObject.SetActive(true);
            }
        }
    }

    public static void pageInc(bool pgvc)
    {
        if (pgvc) page += 1;
        else page -= 1;
    }
}
