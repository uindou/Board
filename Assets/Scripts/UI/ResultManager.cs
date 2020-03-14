using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public int winReward;
    public int onlineReward;
    // Start is called before the first frame update
    void Awake()
    {
        CoinCulculate();
    }
    void Start()
    {
        WinnerDisplay();
    }
    void CoinCulculate()
    {
        int coin = 0;
        Transform Rewards = this.transform.GetChild(1);
        Rewards.GetChild(0).GetChild(2).GetComponent<Text>().text = "+" + DataBase.bonusCoin.ToString();
        if (DataBase.preStage == "AIStage")
        {
            Rewards.GetChild(0).gameObject.SetActive(true);
            coin += DataBase.bonusCoin;
        }
        else 
        {
            Rewards.GetChild(0).gameObject.SetActive(false);
        }

        if (DataBase.preStage == "AIStage")
        {
            if (!DataBase.winner)
            {
                Rewards.GetChild(1).GetChild(2).GetComponent<Text>().text = "+" + winReward.ToString();
                coin += winReward;
            }
            else
            {
                Rewards.GetChild(1).GetChild(2).GetComponent<Text>().text = "";
                Rewards.GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(false);
            }
        }
        else
        {
            Rewards.GetChild(1).gameObject.SetActive(false);
        }
        Rewards.GetChild(2).GetChild(2).GetComponent<Text>().text = "+" + onlineReward.ToString();
        if (DataBase.isOnline)
        {
            coin += onlineReward;
        }
        else
        {
            Rewards.GetChild(2).GetChild(2).GetComponent<Text>().text = "";
            Rewards.GetChild(2).GetChild(1).GetChild(0).gameObject.SetActive(false);
        }
        CoinManager.SetBonus(coin);
    }
    void WinnerDisplay()
    {
        if (DataBase.preStage == "AIStage" & !DataBase.winner)
        {
            this.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "You Win!";
        }
        else if(DataBase.preStage == "AIStage")
        {
            this.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "You Lose";
        }
        else if (DataBase.preStage == "Game" & DataBase.winner)
        {
            this.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Player2 Win!";
        }
        else
        {
            this.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Player1 Win!";
        }
    }

}
