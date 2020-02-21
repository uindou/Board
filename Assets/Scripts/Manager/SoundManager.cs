﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Play()
    {
        if (DataBase.bgmflug)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
    void Start()
    {
        Play();
    }
    public void OnClick()
    {
        DataBase.bgmflug = !DataBase.bgmflug;
        Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
