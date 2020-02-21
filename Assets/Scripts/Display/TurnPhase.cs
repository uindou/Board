using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnPhase : MonoBehaviour
{
    public bool turn ;
    public bool phase;
    /*
    private Transform turnObj;
    private Transform phaseObj;
    */
    private Transform movephase;
    private Transform attackphase;
    private Transform yourturn;
    private Transform enemyturn;
    public void turnUpdate(bool turn)
    {
        /*
        if(turn)turnObj.GetComponent<Text>().text = "ENEMY TURN";
        else turnObj.GetComponent<Text>().text = "YOUR TURN";
        */
        if (turn)
        {
            yourturn.gameObject.SetActive(true);
            enemyturn.gameObject.SetActive(false);
        }
        else
        {
            yourturn.gameObject.SetActive(false);
            enemyturn.gameObject.SetActive(true);
        }
    }
    public void PhaseUpdate(bool isMove)
    {
        /*
        if (isMove) phaseObj.GetComponent<Text>().text = "MOVE PHASE";
        else phaseObj.GetComponent<Text>().text = "ATTACK PHASE";
        */
        if (isMove)
        {
            movephase.gameObject.SetActive(false);
            attackphase.gameObject.SetActive(true);
        }
        else
        {
            movephase.gameObject.SetActive(true);
            attackphase.gameObject.SetActive(false);
        }
    }
    public void GameEnd(bool turn)
    {
        /*
        turnObj.GetComponent<Text>().text = "GAME END";
        if(turn) phaseObj.GetComponent<Text>().text = "YOU WIN";
        else phaseObj.GetComponent<Text>().text = "YOU LOSE";
        */
    }
    void Start()
    {
        /*
        turnObj = this.gameObject.transform.GetChild(0).transform.GetChild(0);
        phaseObj = this.gameObject.transform.GetChild(1).transform.GetChild(0);
        */
        movephase = this.gameObject.transform.GetChild(1).transform.GetChild(0).transform.GetChild(1);
        attackphase = this.gameObject.transform.GetChild(1).transform.GetChild(1).transform.GetChild(1);
        yourturn = this.gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1);
        enemyturn = this.gameObject.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
