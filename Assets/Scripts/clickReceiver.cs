﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static gameManage;

public class clickReceiver : MonoBehaviour
{
    private float duration = 1.0f;
    public bool trigger;
    public void OnClick()
    {
        Debug.Log("Clicked");
        gameManage.requestEnqueue(this.gameObject);
    }
   public void Flash()
    {
        Debug.Log("送信");
        StartCoroutine("testtimer", 5);
    }
    IEnumerator testtimer(int lefttime)
    {
        while (lefttime >= 0)
        {
            // 残り時間が0以上の場合はタイマーを更新　
            yield return new WaitForSeconds(0.05f);
            float phi = Time.time / duration * 2 * Mathf.PI;
            float amplitude = Mathf.Cos(phi) * 0.5F + 0.5F;
            this.GetComponent<Image>().color = new Color(amplitude, 170, amplitude);
        }
        Debug.Log("end");
    }
}
