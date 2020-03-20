using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeLimitPanalManager : MonoBehaviour
{
    public Text countDown;
    private DateTime nowTime;
    private DateTime startTime;
    public int timeLimit = 3;
    public Button paretButton;
    private int duration;
    
    // Start is called before the first frame update
    void Start()
    {
        paretButton.interactable = false;
    }

    private void OnEnable()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!PlayerPrefs.HasKey("rewardAdLimitStart"))
        {
            this.gameObject.SetActive(false);
            paretButton.interactable = true;
        }

        else
        {
            nowTime = DateTime.Now;
            startTime = DateTime.FromBinary(System.Convert.ToInt64(PlayerPrefs.GetString("rewardAdLimitStart")));
            duration = (int)(nowTime - startTime).TotalMinutes;
            countDown.GetComponent<Text>().text = (timeLimit - duration).ToString();
            if (duration >= timeLimit)
            {
                this.gameObject.SetActive(false);
                paretButton.interactable = true;
            }
        }

    }
}
