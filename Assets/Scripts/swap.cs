using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static gameManage;

public class swap : MonoBehaviour
{
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnClick()
    {
        gameManage.requestEnqueue(this.gameObject);
        GameObject obj1 = this.gameObject;
        GameObject obj2 = obj;
        Vector2 A_POS = obj1.transform.position;
        Vector2 B_POS = obj2.transform.position;
        obj1.transform.position = B_POS;
        obj2.transform.position = A_POS;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
