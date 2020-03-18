using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinTabSetting : MonoBehaviour
{
    public int mode;
    public void OnClick()
    {
        SkinViewManager.nowSkin = mode;
    }
}
