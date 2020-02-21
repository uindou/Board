using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    public static int coin;
    public static int bonus;
    public static int GetCoin()
    {
        coin = PlayerPrefs.GetInt("coin", 0);
        return coin;
    }
    public static void Reset()
    {
        PlayerPrefs.SetInt("coin", 0);
        coin = 0;
    }
    public static void SetCoin(int bonus)
    {
        coin += bonus;
        PlayerPrefs.SetInt("coin", coin);
    }
    public static void SetBonus(int bonusCoin)
    {
        bonus = bonusCoin;
    }
    private void Start()
    {
        
        switch (SceneManager.GetActiveScene().name)
        {
            case "Win":
                this.GetComponent<Text>().text = "+" + bonus.ToString();
                SetCoin(bonus);
                break;
            case "MainMenu":
                GetCoin();
                this.GetComponent<Text>().text = coin.ToString();
                break;
            default:
                break;

        }
        
    }
}
