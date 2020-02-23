using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WindowCloser : MonoBehaviour
{
    public GameObject closeWindow;
    public GameObject closeWindow2 = null
        ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        closeWindow.SetActive(false);
        closeWindow2.SetActive(false);
    }
}
