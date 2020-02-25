using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WindowCloser : MonoBehaviour
{
    public GameObject closeWindow;
    public GameObject closeWindow2;
    public GameObject targetWindow;
    public GameObject targetTab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Close1()
    {
        closeWindow.SetActive(false);
    }

    public void Close2()
    {
        closeWindow2.SetActive(false);
    }

    public void TabInit()
    {
        targetWindow.transform.SetAsLastSibling();
        targetTab.GetComponent<Image>().color = Color.white;
    }
}
