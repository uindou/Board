using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public string winner;
    // Start is called before the first frame update
    void Start()
    {

        if (winner == "Player1")
        {
            this.transform.GetChild(0).GetComponent<Text>().text = "Player1 Win!";
        }
        else if(winner == "Player2")
        {
            this.transform.GetChild(0).GetComponent<Text>().text = "Player2 Win!";
        }
        else
        {
            this.transform.GetChild(0).GetComponent<Text>().text = "You Win!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
