using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public GameObject closeWindow;
    // Start is called before the first frame update
    public void OnClick()
    {
        Debug.Log(1);
        PlayerPrefs.SetInt("coin", 0);
        CoinManager.GetCoin();
        closeWindow.SetActive(false);
    }
}
