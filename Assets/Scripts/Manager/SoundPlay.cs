using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    public AudioSource audio;
    // Update is called once per frame
    void Start()
    {
        if (DataBase.bgmflug) audio.Play();
        else audio.Stop();
    }
}
