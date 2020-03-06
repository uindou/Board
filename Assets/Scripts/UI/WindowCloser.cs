using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WindowCloser : MonoBehaviour
{
    public GameObject closeWindow;
    public GameObject closeWindow2;
    public GameObject closeWindow3;
    public GameObject targetWindow;
    public GameObject targetTab;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("押された");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Close1()
    {
        Debug.Log("押された");
        closeWindow.SetActive(false);
    }

    public void Close2()
    {
        Debug.Log("押された");
        closeWindow2.SetActive(false);
    }

    public void Close3()
    {
        Debug.Log("押された");
        closeWindow3.SetActive(false);
    }

    public void TabInit()
    {
        Debug.Log("押された");
        targetWindow.transform.SetAsLastSibling();
        targetTab.GetComponent<Image>().color = Color.white;
    }
}
