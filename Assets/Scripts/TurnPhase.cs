using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnPhase : MonoBehaviour
{
    public bool turn ;
    public bool phase;
    private Transform turnObj;
    private Transform phaseObj;
    public void turnUpdate(bool turn)
    {
        if(turn)turnObj.GetComponent<Text>().text = "ENEMY TURN";
        else turnObj.GetComponent<Text>().text = "YOUR TURN";
    }
    public void PhaseUpdate(bool isMove)
    {
        if (isMove) phaseObj.GetComponent<Text>().text = "MOVE PHASE";
        else phaseObj.GetComponent<Text>().text = "ATTACK PHASE";
    }
    void Start()
    {
        turnObj = this.gameObject.transform.GetChild(0).transform.GetChild(0);
        phaseObj = this.gameObject.transform.GetChild(1).transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
