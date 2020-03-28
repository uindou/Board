using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageWindowManager : MonoBehaviour
{
    public GameObject targetWindow;
    public void OnClick()
    {
        targetWindow.SetActive(true);
        StageSelectManager.MyInit();
        StageSelectManager.Draw();

    }
}
