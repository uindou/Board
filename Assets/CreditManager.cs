using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditManager : MonoBehaviour
{   
    private GameObject thisWindow;
    private GameObject creditText;
    public Vector3 startVector;
    public Vector3 endVector;
    public float scrollSpeed;

    // Start is called before the first frame update
    void Start()
    {
        thisWindow = this.gameObject;
        creditText = this.transform.GetChild(0).gameObject;
        //creditText.transform.position = startVector;
        Scroll();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Scroll()
    {
        while (false)
        {
            creditText.transform.position += new Vector3(0, scrollSpeed, 0);
        }
    }
}
