using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayinMain : MonoBehaviour
{
    public AudioSource audio;
    private bool flug;
    // Update is called once per frame
    void Update()
    {
        if (DataBase.bgmflug != flug) 
        {
            if(DataBase.bgmflug) audio.Play();
            else audio.Stop();
            flug = DataBase.bgmflug;
        }
        
    }
}
