﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SmokeCoatingManager : MonoBehaviour
{
    public GameObject smokeLayer;
    public GameObject targetWindow0;
    public GameObject targetWindow1;
    public GameObject targetWindow2;
    public GameObject targetWindow3;
    public GameObject targetWindow4;
    public GameObject targetWindow5;
    private bool trigger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        trigger = targetWindow0.activeSelf
                  || targetWindow1.activeSelf
                  || targetWindow2.activeSelf
                  || targetWindow3.activeSelf
                  || targetWindow4.activeSelf
                  || targetWindow5.activeSelf;
        smokeLayer.gameObject.SetActive(trigger);
    }
}
