using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        if (DataBase.preStage=="AIStage1")
        {
            this.transform.GetChild(0).GetComponent<Text>().text = "You Win!";
            
        }
        else if(DataBase.preStage=="Game" & DataBase.winner)
        {
            this.transform.GetChild(0).GetComponent<Text>().text = "Player2 Win!";
        }
        else
        {
            this.transform.GetChild(0).GetComponent<Text>().text = "Player1 Win!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
