using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static StageSelectManager;

public class StageSelectButton : MonoBehaviour
{
    public bool vector;
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
        StageSelectManager.StageRotate(vector);
    }

}
