using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnPhase : MonoBehaviour
{
    public bool turn ;
    public bool phase;
    
    // Start is called before the first frame update
    void Start()
    {
        if (turn)
        {
            this.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "YOUR TURN";
        }
        else
        {
            this.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "ENEMY TURN";
        }

        if (phase)
        {
            this.gameObject.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = "MOVE PHASE";
        }
        else
        {
            this.gameObject.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = "ATTACK PHASE";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
