using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinPageText : MonoBehaviour
{
    static GameObject ThisObj;
    private void Start()
    {
        ThisObj = this.gameObject;
        PageChange(1);
    }
    // Start is called before the first frame update
    public static void PageChange(int text)
    {
        ThisObj.GetComponent<Text>().text = text+"/3";
    }
}
