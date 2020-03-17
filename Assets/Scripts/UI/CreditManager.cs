using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;

public class CreditManager : MonoBehaviour
{
    public GameObject targetText;
    public GameObject ghostText;
    public GameObject rewardWindow;
    private Vector3 startVector;
    //　テキストのスクロールスピード
    public float textScrollSpeed = 100;
    //　テキストの制限位置
    private float limitPosition;
    //　エンドロールが終了したかどうか
    private bool isStopEndRoll;
    bool firstFlag;
    

    void Start()
    {
        firstFlag = PlayerPrefs.GetInt("endFirst",0)==0;
        limitPosition = ghostText.transform.position.y;
    }

    private void OnEnable()
    {
        isStopEndRoll = false;
        //targetText.transform.position = new Vector3(751.9f, 746.4f, 0.0f);
        startVector = targetText.transform.position;
    }

    private void OnDisable()
    {
        targetText.transform.position = startVector;

    }
    // Update is called once per frame


    void Update()
    {
        Debug.Log(targetText.transform.position);
   
     //　エンドロール用テキストがリミットを越えるまで動かす
    if (targetText.transform.position.y <= limitPosition)
        {
         targetText.transform.position += new Vector3(0, textScrollSpeed * Time.deltaTime,0);
        }
    else
        {
            IsStopEndroll();
        }
    }

    private async void IsStopEndroll()
    {
        await Task.Delay(1000);
        firstFlag = PlayerPrefs.GetInt("endFirst", 0) == 0;
        PlayerPrefs.SetInt("endFirst", 1);
        if (firstFlag)
        {
            rewardWindow.gameObject.SetActive(true);
            CoinManager.SetBonus(500);
            CoinManager.SetCoin();
            firstFlag = false;
        }
        this.gameObject.SetActive(false);
    }
}
