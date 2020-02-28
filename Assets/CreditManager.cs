using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditManager : MonoBehaviour
{
    public GameObject targetText;
    //　テキストのスクロールスピード
    private float textScrollSpeed = 1000;
    //　テキストの制限位置
    private float limitPosition = 3451f;
    //　エンドロールが終了したかどうか
    private bool isStopEndRoll;
    bool firstFlag;

    void Start()
    {
        firstFlag = PlayerPrefs.GetInt("endFirst",0)==0;
    }

    private void OnEnable()
    {
        isStopEndRoll = false;
        targetText.transform.position = new Vector3(751.9f, 677.5f, 0.0f);
    }
    // Update is called once per frame


    void Update()
    {
        Debug.Log(targetText.transform.position);
        //　エンドロールが終了した時
        if (isStopEndRoll)
        {
            
            firstFlag = PlayerPrefs.GetInt("endFirst", 0) == 0;
            PlayerPrefs.SetInt("endFirst", 1);
            if (firstFlag)
            {
                CoinManager.SetBonus(500);
                CoinManager.SetCoin();
                firstFlag = false;
            }

            this.gameObject.SetActive(false);
        }
        else
        {
            //　エンドロール用テキストがリミットを越えるまで動かす
            if (targetText.transform.position.y <= limitPosition)
            {
                targetText.transform.position += new Vector3(0, textScrollSpeed * Time.deltaTime,0);
            }
            else
            {
                isStopEndRoll = true;
            }
        }
    }
}
