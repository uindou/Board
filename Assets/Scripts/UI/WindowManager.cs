using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowManager : MonoBehaviour
{
    public GameObject targetWindow;

    

    public void OnClick()
    {
        targetWindow.SetActive(true);
    }

}
